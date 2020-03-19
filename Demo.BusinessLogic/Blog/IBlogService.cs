using Demo.Data;
using Demo.Entity;
using Demo.Model.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLogic
{
    public partial interface IBlogService : IBaseService<Post>
    {
        Task<List<PostForSitemapModel>> GetAllPostsForSitemapAsync();
        Task<List<PostModel>> GetRelatedPostsAsync(RelatedPostFindRequest request);
        Task<List<PostModel>> GetPopularPostsAsync(int top = 5);
        Task<List<PostModel>> GetFeaturePostsAsync(int top = 3);
        Task<PostModel> GetPostAsync(string slug);
        Task<BaseFindResponse<PostModel>> SearchPostsAsync(PostSearchRequest request);
    }
}
