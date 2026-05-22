using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IItemService _itemService;

        public CharacterService(ICharacterRepository characterRepository, IItemService itemService)
        {
            _characterRepository = characterRepository;
            _itemService = itemService;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            try
            {
                return await _characterRepository.GetAllAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Character> GetById(int id)
        {
            try
            {
                return await _characterRepository.GetByIdAsync(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task Create(Character character, int userId)
        {
            try
            {
                await _characterRepository.AddAsync(character, userId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task Update(Character character)
        {
            try
            {
                await _characterRepository.UpdateAsync(character);
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _characterRepository.DeleteAsync(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<Dungeon>> DungeonReadyCheck(List<Dungeon> allDungeons, Character specificCharacter)
        {
            List<Dungeon> checkedDungeons = new List<Dungeon>();
            List<Item> characterItems = await _itemService.GetItemsByCharacterIdAsync(specificCharacter.Id);

            int? slotCheckAmount = characterItems.Sum(i => i.SocketAmount ?? 0);

            foreach (Dungeon checkDungeon in allDungeons)
            {
                //Checks required slot amount for each dungeon in the list can also be done by name
                switch (checkDungeon.Id)
                {
                    case 1:
                        if (10 < slotCheckAmount)
                        {
                            checkedDungeons.Add(checkDungeon);
                        }
                        break;
                    case 2:
                        if (20 < slotCheckAmount)
                        {
                            checkedDungeons.Add(checkDungeon);
                        }
                        break;
                    case 3:
                        if (30 < slotCheckAmount)
                        {
                            checkedDungeons.Add(checkDungeon);
                        }
                        break;
                }
            }
            return checkedDungeons;
        }

        public async Task<IEnumerable<Character>> GetCharactersByUserId(int userId)
        {
            try
            {
                return await _characterRepository.GetCharactersByUserId(userId);
            } catch (SqlException)
            {
                throw;
            } catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task EquipItem(int characterId, int oldItemId, int newItemId)
        {
            await _characterRepository.EquipItemAsync(characterId, oldItemId, newItemId);
        }

        public async Task<string> GetSpecNameById(int id)
        {
            try
            {
                return await _characterRepository.GetSpecNameById(id);
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task<int> GetCharacterCount()
        {
            try
            {
                return await  _characterRepository.GetCharacterCount();
            }catch(SqlException)
            {
                throw;
            }
        }
    }
}
