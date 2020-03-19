using Demo.Data;
using Demo.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLogic
{
    public partial interface IUserService
    {
        Task<BaseFindResponse<User>> FindAsync(UserFindRequest request);
        Task<List<Reference>> GetReferencesAsync();
    }
}