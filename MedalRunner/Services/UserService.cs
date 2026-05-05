using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class UserService : IUserService
    {
        private readonly IUserService _userService;

        public UserService(IUserService userService)
        {
            _userService = userService;
        }
    }
}
