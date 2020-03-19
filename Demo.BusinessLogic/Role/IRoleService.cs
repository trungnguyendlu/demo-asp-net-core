using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<BaseFindResponse<Role>> FindAsync(RoleFindRequest request);
        Task<List<Reference>> GetReferencesAsync();
    }
}