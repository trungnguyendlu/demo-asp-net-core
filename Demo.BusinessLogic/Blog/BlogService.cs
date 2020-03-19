using AutoMapper;
using Demo.Data;
using Demo.Entity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Util;
using Demo.Repository;
using Demo.Model.Post;
using Demo.Model.Category;

namespace Demo.BusinessLogic
{
    public partial class BlogService : BaseService<Post>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICacheHelper _cacheHelper;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository,
            ICategoryRepository categoryRepository,
            ICacheHelper cacheHelper,
            IMapper mapper)
            : base(blogRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _cacheHelper = cacheHelper;
            _mapper = mapper;
        }

        public async Task<List<PostForSitemapModel>> GetAllPostsForSitemapAsync()
        {
            return _mapper.Map<List<PostForSitemapModel>>(await _blogRepository.GetAllPostsForSitemapAsync());
        }

        public async Task<BaseFindResponse<PostModel>> SearchPostsAsync(PostSearchRequest request)
        {
            var posts = _mapper.Map<BaseFindResponse<PostModel>>(await _blogRepository.SearchPostsAsync(request));            
            await Populate(posts.Results);
            return posts;
        }

        public async Task<List<PostModel>> GetRelatedPostsAsync(RelatedPostFindRequest request)
        {
            var posts = _mapper.Map<List<PostModel>>(await _blogRepository.GetRelatedPostsAsync(request));
            await Populate(posts);
            return posts;
        }


        public Task<List<PostModel>> GetPopularPostsAsync(int top = 5)
        {
            return _cacheHelper.GetOrSetAsync(CacheKey.GetPopularPosts, async () =>
            {
                var posts = _mapper.Map<List<PostModel>>(await _blogRepository.GetPopularPostsAsync(top));
                await Populate(posts);
                return posts;
            });
        }

        public Task<List<PostModel>> GetFeaturePostsAsync(int top = 3)
        {
            return _cacheHelper.GetOrSetAsync(CacheKey.GetFeaturePosts, async () =>
            {
                var posts = _mapper.Map<List<PostModel>>(await _blogRepository.GetFeaturePostsAsync(top));
                await Populate(posts);
                return posts;
            });
        }

        public async Task<PostModel> GetPostAsync(string slug)
        {
            var post = _mapper.Map<PostModel>(await _blogRepository.GetPostBySlugAsync(slug));
            await Populate(post);
            return post;
        }

        private async Task Populate(List<PostModel> posts)
        {
            var categoryIds = posts.Select(a => a.CategoryId).ToList();
            var categories = _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetCategoriesByIdsAsync(categoryIds));

            foreach (var item in posts)
            {
                item.Category = categories.FirstOrDefault(a => a.Id == item.CategoryId);
            }
        }

        private async Task Populate(PostModel post)
        {
            if (post == null)
            {
                return;
            }

            post.Category = _mapper.Map<CategoryModel>(await _categoryRepository.GetByIdAsync(post.CategoryId));
        }
    }
}
