using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using MongoDB.Bson;

namespace Demo.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<SaveResponse<ObjectId>> SaveAsync(SaveRequest<T> request, Action<T, T> updateAction = null);
        T GetById(ObjectId id);
        Task<T> GetByIdAsync(ObjectId id);
        Task<List<T>> GetAllAsync();
        Task<BaseResponse> DeleteAsync(ObjectId id);
        int Count();
        Task<int> CountAsync();
    }
}