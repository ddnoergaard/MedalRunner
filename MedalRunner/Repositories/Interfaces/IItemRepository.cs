using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task AddItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(int id);
        Task<List<Item>> GetAllItemAsync();
        Task<Item> GetByItemId(int id);
        Task<IEnumerable<Item>> GetItemsByDungeonId(int id);
        Task<string> GetItemSlotNameAsync(int id);
    }
}
