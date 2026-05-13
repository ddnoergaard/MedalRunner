using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Public_pages.Item
{
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;
        public List<Models.Item> _items;

        public IndexModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task OnGetAsynch()
        {
            _items = (await _itemService.GetAllItem()).ToList();
        }
    }
}
}
