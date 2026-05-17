using MedalRunner.Repositories;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.Item
{
    public class DeleteModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public Models.Item Item { get; set; }

        public DeleteModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        // CHANGED: was empty - now loads the item so the confirmation page can display its name
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var items = await _itemService.GetAllItemsWithSourceAsync();
            Item = items.FirstOrDefault(i => i.Id == id);
            if (Item == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        // CHANGED: was named DeleteItemOnPost() which Razor Pages never calls - renamed to OnPost()
        public IActionResult OnPost()
        {
            var deleteItem = _itemService.DeleteItem(Item.Id);
            if (deleteItem == null)
            {
                return RedirectToPage("/Pages/Error");
            }

            return RedirectToPage("/Admin/Item/Index");
        }
    }
}
