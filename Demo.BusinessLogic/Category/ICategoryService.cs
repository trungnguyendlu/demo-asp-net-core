using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entity;
using Demo.Model.Category;

namespace Demo.BusinessLogic
{
    public partial interface ICategoryService : IBaseService<Category>
    {
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel> GetCategoryAsync(string slug);
    }
}