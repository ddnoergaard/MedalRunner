using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IItemService
    {
        Task AddItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(int id);
        Task<Item?> GetByItemId(int id);
        Task<IEnumerable<Item>> GetAllItem();
        Task<IEnumerable<Item>> GetItemsByDungeonIdAsync(int id);
    }
}
