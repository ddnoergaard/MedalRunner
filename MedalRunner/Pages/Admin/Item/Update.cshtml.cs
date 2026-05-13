using MedalRunner.Repositories;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.Item
{
    public class UpdateModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public Models.Item Item { get; set; }

        public UpdateModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult OnGet(int id)
        {
            Item = _itemService.GetAllItem().Result.FirstOrDefault(i => i.Id == id);
            if (Item == null)
            {
                return RedirectToPage("/NotFound");
            }                

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _itemService.UpdateItem(Item);
            return RedirectToPage("GetAllItem");
        }
    }
}
