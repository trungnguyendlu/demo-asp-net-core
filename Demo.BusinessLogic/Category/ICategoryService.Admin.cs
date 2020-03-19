using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public partial interface ICategoryService
    {
        Task<BaseFindResponse<Category>> FindAsync(CategoryFindRequest request);
        Task<List<Reference>> GetReferencesAsync();
    }
}