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
            catch (SqlException ex) 
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("User with that email already exists");
                }
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

        public async Task DeleteUserById(int userId)
        {
            try
            {
                await _userRepository.DeleteUserById(userId);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<int> GetUserCount()
        {
            try
            {
                return await _userRepository.GetUserCount();
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetFiveLatestUsers()
        {
            try
            {
                return await _userRepository.GetFiveLatestUsers();
            } catch (SqlException)
            {
                throw;
            }
        }
    }
}
