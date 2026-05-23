using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        Task<User> GetUserByEmail(string Email);

        Task DeleteUserById(int id);
        Task<int> GetUserCount();
        Task<IEnumerable<User>> GetFiveLatestUsers();
    }
}
