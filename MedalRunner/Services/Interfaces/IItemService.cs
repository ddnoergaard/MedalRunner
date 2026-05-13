using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IItemService
    {
        Task AddItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(int id);
        Task<Item?> GetByItemId(int id);
        Task<List<Item>> GetAllItems();
        Task<IEnumerable<Item>> GetItemsByDungeonIdAsync(int id);
    }
}
