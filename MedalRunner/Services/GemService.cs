using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class GemService : IGemService
    {
        private readonly IGemRepository _gemRepository;

        public GemService(IGemRepository gemRepository)
        {
            _gemRepository = gemRepository;
        }
    }
}
