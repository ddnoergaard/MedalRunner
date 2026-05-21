using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IEnumerable<Subscription>> GetAllSub()
        {
            try
            {
                return await _subscriptionRepository.GetAllSub();
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}
