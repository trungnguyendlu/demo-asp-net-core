using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using MongoDB.Bson;

namespace Demo.Repository
{
    public partial interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<BaseFindResponse<Category>> FindAsync(CategoryFindRequest request);
        Task<List<Reference>> GetReferencesAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Category>> GetCategoriesByIdsAsync(List<ObjectId> ids);
        Task<Category> GetCategoryBySlugAsync(string slug);
    }
}