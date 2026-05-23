using MedalRunner.Models;
using MedalRunner.Services;

namespace MedalRunner.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsync();
        Task<Character> GetByIdAsync(int id);
        // Returns the new character's ID so the service layer can act on it.
        Task<int> AddAsync(Character character, int userId);
        Task UpdateAsync(Character character);
        Task DeleteAsync(int id);
        Task<IEnumerable<Character>> GetCharactersByUserId(int userId);
        Task EquipItemAsync(int characterId, int oldItemId, int newItemId);
        Task<string> GetSpecNameById(int id);
    }
}
