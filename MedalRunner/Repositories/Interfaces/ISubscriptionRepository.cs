using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        public Task<IEnumerable<Subscription>> GetAllSub();
    }
}
