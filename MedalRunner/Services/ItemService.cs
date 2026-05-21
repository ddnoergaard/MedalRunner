using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

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
                return await _itemRepository.GetAllItemAsync();
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Item>> GetAllItemsWithSourceAsync()
        {
            return await _itemRepository.GetAllItemsWithSourceAsync();
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

        public async Task<IEnumerable<Item>> GetItemsByDungeonIdAsync(int id)
        {
            try
            {
                return await _itemRepository.GetItemsByDungeonId(id);
            } catch (Exception ex)
            {
                throw;
            }
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

        public async Task<string> GetItemSlotNameAsync(int id)
        {
            try
            {
                return await _itemRepository.GetItemSlotNameAsync(id);
            } catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<List<Item>> GetItemsByCharacterIdAsync(int characterId)
        {
            try
            {
                return await _itemRepository.GetItemsByCharacterIdAsync(characterId);
            } catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Item>> GetRandomItemsForEachSlot()
        {
            List<Item> allItems = (await _itemRepository.GetAllItemAsync()).ToList();

            List<Item> returnList = new List<Item>();

            List<int> slotInts = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            Random rand = new Random();

            int count = 0;

            List<Item> searchList = new List<Item>();

            while (true)
            {
                if (count == 11 || count == 13) count++;
                count++;
                
                searchList = allItems.Where(i => i.Slot == count).ToList();
                int randomNumber = rand.Next(1, searchList.Count);
                returnList.Add(searchList[randomNumber - 1]);
                if (count == 16) break;
                

            }
            return returnList;
        }

    }
}
