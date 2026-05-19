using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(User user)
        {
            try
            {
                await _userRepository.AddUser(user);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            try
            {
                return await _userRepository.GetUserByEmail(Email);
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
