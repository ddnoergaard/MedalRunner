using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public Task<IEnumerable<Subscription>> GetAllSub();
    }
}
