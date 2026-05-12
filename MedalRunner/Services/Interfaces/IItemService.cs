using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItem();
        Task AddItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(int id);
        Task<Item?> GetByItemId(int id);
    }
}
