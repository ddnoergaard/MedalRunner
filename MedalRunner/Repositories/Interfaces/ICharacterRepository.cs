using MedalRunner.Models;
using MedalRunner.Services;

namespace MedalRunner.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsynch();
        Task<Character> GetByIdAsynch(int id);
        Task AddAsynch(Character character);
        Task UpdateAsynch(Character character);
        Task DeleteAsynch(int id);

    }
}
