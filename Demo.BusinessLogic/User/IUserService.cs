using Demo.Entity;
using System.Threading.Tasks;

namespace Demo.BusinessLogic
{
    public partial interface IUserService : IBaseService<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> LoginAsync(string email, string password);
        Task UpdateLastLoginDateAsync(string email);
    }
}