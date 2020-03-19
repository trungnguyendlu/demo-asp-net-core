using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Repository;

namespace Demo.BusinessLogic
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
            : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<BaseFindResponse<Role>> FindAsync(RoleFindRequest request)
        {
            return _roleRepository.FindAsync(request);
        }

        public Task<List<Reference>> GetReferencesAsync()
        {
            return _roleRepository.GetReferencesAsync();
        }
    }
}
