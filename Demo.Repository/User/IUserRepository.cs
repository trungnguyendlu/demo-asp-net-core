using Demo.Data;
using Demo.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public partial interface IUserRepository : IBaseRepository<User>
    {
        Task<BaseFindResponse<User>> FindAsync(UserFindRequest request);
        Task<List<Reference>> GetReferencesAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> LoginAsync(string email, string password);
        Task UpdateLastLoginDateAsync(string email);
    }
}