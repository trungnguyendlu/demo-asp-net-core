using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        protected readonly IMongoCollection<T> collection;

        public BaseRepository(DatabaseContext context, IMongoCollection<T> collection)
        {
            this.context = context;
            this.collection = collection;
        }

        protected abstract string Title { get; }

        public async Task<SaveResponse<ObjectId>> SaveAsync(SaveRequest<T> request, Action<T, T> populateAction = null)
        {
            try
            {
                if (!await ValidateDataDuplicate(request.Entity))
                {
                    return SaveResponse<ObjectId>.FailResponse(request.Entity.Id,
                            $"Dữ liệu đã tồn tại trong hệ thống");
                }

                await PopulateSaveAsync(request, populateAction);

                var success = false;
                if (request.IsEdit)
                {
                    var response = await collection.ReplaceOneAsync(a => a.Id == request.Entity.Id, request.Entity);
                    success = response.IsModifiedCountAvailable;
                }
                else
                {
                    await collection.InsertOneAsync(request.Entity);
                    success = true;
                }

                if (!success)
                {
                    return SaveResponse<ObjectId>.FailResponse(request.Entity.Id,
                        $"Đã xảy ra lỗi trong quá trình lưu {Title}");
                }

                return SaveResponse<ObjectId>.SuccessResponse(request.Entity.Id, $"Lưu {Title} thành công");
            }
            catch (Exception ex)
            {
                return SaveResponse<ObjectId>.FailResponse(request.Entity.Id,
                    ex.Message);
            }
        }

        public T GetById(ObjectId id)
        {
            return collection.Find(a => a.Id == id).FirstOrDefault();
        }

        public Task<T> GetByIdAsync(ObjectId id)
        {
            return collection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAllAsync()
        {
            return collection.Find(a => true).ToListAsync();
        }

        public async Task<BaseResponse> DeleteAsync(ObjectId id)
        {
            if (await IsInUseAsync(id))
            {
                return BaseResponse.FailResponse($"{Title} đã được sử dụng");
            }

            var response = await collection.DeleteOneAsync(a => a.Id == id);
            if (response.DeletedCount == 0)
            {
                return BaseResponse.FailResponse($"Đã xảy ra lỗi trong quá trình xóa {Title}");
            }
            return BaseResponse.SuccessResponse($"Xóa {Title} thành công");
        }

        public int Count()
        {
            return (int)collection.CountDocuments(a => true);
        }

        public async Task<int> CountAsync()
        {
            return (int)await collection.CountDocumentsAsync(a => true);
        }

        protected virtual Task<bool> IsInUseAsync(ObjectId id)
        {
            return Task.FromResult(false);
        }

        protected virtual Task<bool> ValidateDataDuplicate(T entity)
        {
            return Task.FromResult(true);
        }

        private async Task PopulateSaveAsync(SaveRequest<T> request, Action<T, T> populateAction = null)
        {
            var currentDate = DateTime.Now;
            if (!request.IsEdit)
            {
                request.Entity.CreatedUserId = request.UserId;
                request.Entity.CreatedDate = currentDate;
                request.Entity.UpdatedUserId = request.UserId;
                request.Entity.UpdatedDate = currentDate;
            }
            else
            {
                request.Entity.UpdatedUserId = request.UserId;
                request.Entity.UpdatedDate = currentDate;
                var dbObject = await GetByIdAsync(request.Entity.Id);
                if (dbObject != null)
                {
                    populateAction?.Invoke(dbObject, request.Entity);
                    request.Entity.CreatedDate = dbObject.CreatedDate;
                    request.Entity.CreatedUserId = dbObject.CreatedUserId;
                }
            }
        }
    }
}
