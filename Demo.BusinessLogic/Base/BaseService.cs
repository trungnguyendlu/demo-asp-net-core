using System;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Repository;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Demo.BusinessLogic
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> repository;

        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }

        public Task<SaveResponse<ObjectId>> SaveAsync(SaveRequest<T> request, Action<T, T> populateAction = null)
        {
            return repository.SaveAsync(request, populateAction);
        }

        public T GetById(ObjectId id)
        {
            return repository.GetById(id);
        }

        public Task<T> GetByIdAsync(ObjectId id)
        {
            return repository.GetByIdAsync(id);
        }

        public Task<List<T>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<BaseResponse> DeleteAsync(ObjectId id)
        {
            return repository.DeleteAsync(id);
        }

        public int Count()
        {
            return repository.Count();
        }

        public Task<int> CountAsync()
        {
            return repository.CountAsync();
        }
    }
}
