using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
