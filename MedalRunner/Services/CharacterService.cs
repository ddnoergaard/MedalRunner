using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            try
            {
                return await _characterRepository.GetAllAsync();
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task Create(Character character)
        {
            try
            {
                await _characterRepository.AddAsync(character);
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                throw;
            }
        }
    
        public async Task<List<Dungeon>> DungeonReadyCheck(List<Dungeon> allDungeons, Character specificCharacter)
        {
            int? slotCheckAmount = 0;


            List<Dungeon> checkedDungeons = new List<Dungeon>();
            Character character = await _characterRepository.GetByIdAsync(specificCharacter.Id);
            List<Item> characterItems = new List<Item>();
            
            //Adds Amount of gem slots to slotCeckAmount and returns Character items to the list characterItems
            slotCheckAmount += ReturnCountItems(character.Gear, characterItems);
            slotCheckAmount += ReturnCountItems(character.Weapon, characterItems);

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

        // Helper method to count socket slots and bring together gear and weapons
        private int? ReturnCountItems(List<Item> items, List<Item> returnList)
        {
            int? slotAmount = 0;
            foreach(Item item in items)
            {
                returnList.Add(item);

                slotAmount += item.SocketAmount;
            }
            return slotAmount;
        }
    }
}
