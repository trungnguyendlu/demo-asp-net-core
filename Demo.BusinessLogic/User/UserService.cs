using System.Threading.Tasks;
using Demo.Entity;
using Demo.Repository;

namespace Demo.BusinessLogic
{
    public partial class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _userRepository.GetByEmailAsync(email);
        }

        public Task<User> LoginAsync(string email, string password)
        {
            return _userRepository.LoginAsync(email, password);
        }

        public Task UpdateLastLoginDateAsync(string email)
        {
            return _userRepository.UpdateLastLoginDateAsync(email);
        }
    }
}
