using Demo.Data;
using Demo.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLogic
{
    public partial interface IBlogService
    {
        Task<BaseFindResponse<Post>> FindAsync(PostFindRequest request);
        Task<List<Reference>> GetAsReferencesAsync();
    }
}
