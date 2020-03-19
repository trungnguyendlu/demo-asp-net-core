using Demo.Data;
using Demo.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLogic
{
    public partial class BlogService
    {
        public Task<BaseFindResponse<Post>> FindAsync(PostFindRequest request)
        {
            return _blogRepository.FindAsync(request);
        }

        public Task<List<Reference>> GetAsReferencesAsync()
        {
            return _blogRepository.GetAsReferencesAsync();
        }
    }
}
