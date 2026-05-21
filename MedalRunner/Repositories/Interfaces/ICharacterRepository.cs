using MedalRunner.Models;
using MedalRunner.Services;

namespace MedalRunner.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsync();
        Task<Character> GetByIdAsync(int id);
        Task AddAsync(Character character, int userId);
        Task UpdateAsync(Character character);
        Task DeleteAsync(int id);
        Task<IEnumerable<Character>> GetCharactersByUserId(int userId);
        Task EquipItemAsync(int characterId, int oldItemId, int newItemId);
        Task<string> GetSpecNameById(int id);
    }
}
