using MedalRunner.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Public_pages.Item
{
    public class DetailsModel : PageModel
    {
        private readonly IItemRepository _itemRepository;
        public Models.Item _item { get; set; }

        public DetailsModel(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task OnGetAsync(int id)
        {
            _item = await _itemRepository.GetByItemId(id);
        }
    }
}
}
