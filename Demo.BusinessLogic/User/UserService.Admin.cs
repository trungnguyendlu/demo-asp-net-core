using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public partial class UserService
    {
        public Task<BaseFindResponse<User>> FindAsync(UserFindRequest request)
        {
            return _userRepository.FindAsync(request);
        }
        
        public Task<List<Reference>> GetReferencesAsync()
        {
            return _userRepository.GetReferencesAsync();
        }
    }
}
