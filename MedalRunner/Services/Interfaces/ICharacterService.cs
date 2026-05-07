using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAll();
        Task<Character?> GetById(int id);
        Task Create(Character character);
        Task Update(Character character);
        Task Delete(int id);
    }
}
