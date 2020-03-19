using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public partial class CategoryService
    {
        public Task<BaseFindResponse<Category>> FindAsync(CategoryFindRequest request)
        {
            return _categoryRepository.FindAsync(request);
        }

        public Task<List<Reference>> GetReferencesAsync()
        {
            return _categoryRepository.GetReferencesAsync();
        }
    }
}
