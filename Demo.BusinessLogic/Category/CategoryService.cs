using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Entity;
using Demo.Model.Category;
using Demo.Repository;
using Demo.Util;

namespace Demo.BusinessLogic
{
    public partial class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICacheHelper _cache;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ICacheHelper cache)
            : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            return _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetAllCategoriesAsync());
        }

        public async Task<CategoryModel> GetCategoryAsync(string slug)
        {
            return _mapper.Map<CategoryModel>(await _categoryRepository.GetCategoryBySlugAsync(slug));
        }
    }
}
