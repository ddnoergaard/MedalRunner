using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace MedalRunner.Pages.Admin_pages
{
    public class AddItemModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public Item Item { get; set; }

        public AddItemModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public void OnGet()
        {

        }

        public void OnPost(Item item)
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            _itemService.AddItem(item);
        }
    }
}
