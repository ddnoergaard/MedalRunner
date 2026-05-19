using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(User user);
        Task<User> GetUserByEmail(string Email);
        Task DeleteUserById(int userId);
    }
}
