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

        public void OnGet()
        {

        }

        public IActionResult DeleteOnPost()
        {
            var deleteItem = _itemService.DeleteItem(Item.Id);
            if(deleteItem == null)
            {
                return RedirectToPage("/Pages/Error");
            }

            return RedirectToPage("/Admin/Item/Index");
        }
    }
}
