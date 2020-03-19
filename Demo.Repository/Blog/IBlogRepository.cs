using Demo.Data;
using Demo.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public partial interface IBlogRepository : IBaseRepository<Post>
    {
        Task<BaseFindResponse<Post>> FindAsync(PostFindRequest request);
        Task<BaseFindResponse<Post>> SearchPostsAsync(PostSearchRequest request);
        Task<List<Post>> GetAllPostsForSitemapAsync();
        Task<List<Reference>> GetAsReferencesAsync();
        Task<List<Post>> GetRelatedPostsAsync(RelatedPostFindRequest request);
        Task<List<Post>> GetPopularPostsAsync(int top = 5);
        Task<List<Post>> GetFeaturePostsAsync(int top = 3);
        Task<Post> GetPostBySlugAsync(string slug);
    }
}
