using MedalRunner.Services.Interfaces;
using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Services
{
    public class BossService : IBossService
    {
        private readonly IBossRepository _bossRepository;

        public BossService(IBossRepository bossRepository)
        {
            _bossRepository = bossRepository;
        }
    }
}
