using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<BaseFindResponse<Role>> FindAsync(RoleFindRequest request);
        Task<List<Reference>> GetReferencesAsync();
    }
}