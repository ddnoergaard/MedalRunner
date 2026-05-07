using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
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
    }
}
