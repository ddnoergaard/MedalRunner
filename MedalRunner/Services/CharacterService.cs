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
    }
}
