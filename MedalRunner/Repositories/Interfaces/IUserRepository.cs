using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        Task<User> GetUserByEmail(string Email);
    }
}
