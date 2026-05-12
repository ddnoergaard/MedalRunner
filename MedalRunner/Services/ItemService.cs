using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetAllItem()
        {
            try
            {
                return await _itemRepository.GetAllItemsAsync();
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public async Task AddItem(Item item)
        {
            await _itemRepository.AddItem(item);
        }

        public async Task UpdateItem(Item item)
        {
            await _itemRepository.UpdateItem(item);
        }

        public async Task DeleteItem(int id)
        {
            await _itemRepository.DeleteItem(id);
        }

        public async Task<Item> GetByItemId(int id)
        {
            try
            {
                return await _itemRepository.GetByItemId(id);
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}
