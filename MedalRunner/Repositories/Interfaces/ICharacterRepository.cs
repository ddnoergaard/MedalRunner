using MedalRunner.Models;
using MedalRunner.Services;

namespace MedalRunner.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsync();
        Task<Character> GetByIdAsync(int id);
        Task<int> AddAsync(Character character);
        Task UpdateAsync(Character character);
        Task DeleteAsync(int id);
        // Returns all items equipped by a character via the character_items junction table
        Task<List<Item>> GetItemsByCharacterIdAsync(int characterId);
    }
}
