using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.Item
{
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public List<Models.Item> Items { get; set; }

        public IndexModel(IItemService itemService)
        {
            _itemService = itemService;
        }
        public async Task OnGet()
        {
            Items = (await _itemService.GetAllItem()).ToList();
        }
    }
}
