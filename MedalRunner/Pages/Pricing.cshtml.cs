using MedalRunner.Models;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages
{
    public class PricesModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;

        public List<Subscription> subs { get; set; } = new List<Subscription>();

        public PricesModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task OnGet()
        {
            subs = (await _subscriptionService.GetAllSub()).ToList();
        }
    }
}
