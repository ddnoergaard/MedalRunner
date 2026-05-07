using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class ConsumableService : IConsumableService
    {
        private readonly IConsumableRepository _consumableRepository;

        public ConsumableService(IConsumableRepository consumableRepository)
        {
            _consumableRepository = consumableRepository;
        }
    }
}
