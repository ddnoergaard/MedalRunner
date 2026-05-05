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

        public Task<IEnumerable<Character>> GetAll() => _characterRepository.GetAllAsynch();

        public Task<Character> GetById(int id) => _characterRepository.GetByIdAsynch(id);

        public Task Create(Character character) => _characterRepository.AddAsynch(character);

        public Task Update(Character character) => _characterRepository.UpdateAsynch(character);

        public Task Delete(int id) => _characterRepository.DeleteAsynch(id);
    }
}
