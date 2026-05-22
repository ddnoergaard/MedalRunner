using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAll();
        Task<Character?> GetById(int id);
        Task Create(Character character, int userId);
        Task Update(Character character);
        Task Delete(int id);
        Task<IEnumerable<Character>> GetCharactersByUserId(int userId);
        Task EquipItem(int characterId, int oldItemId, int newItemId);
        Task<string> GetSpecNameById(int id);
        Task<int> GetCharacterCount();
    }
}
