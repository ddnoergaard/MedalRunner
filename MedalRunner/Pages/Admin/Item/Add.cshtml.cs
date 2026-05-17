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
        private readonly IBossService _bossService; // CHANGED: injected to look up boss by name

        [BindProperty]
        public Models.Item Item { get; set; }

        [BindProperty]
        public string? BossName { get; set; } // CHANGED: replaces free-text Source field; bound from the form

        public AddItemModel(IItemService itemService, IBossService bossService)
        {
            _itemService = itemService;
            _bossService = bossService;
        }

        public void OnGet()
        {
        }

        // CHANGED: was synchronous OnPost() with no boss handling - now async, resolves boss name to BossId before saving
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // CHANGED: looks up the boss by name and sets BossId so AddItem can write to boss_drops
            if (!string.IsNullOrWhiteSpace(BossName))
            {
                try
                {
                    var boss = await _bossService.GetBossByNameAsync(BossName);
                    Item.BossId = boss.Id;
                }
                catch (KeyNotFoundException)
                {
                    ModelState.AddModelError("BossName", $"No boss named '{BossName}' was found.");
                    return Page();
                }
            }

            await _itemService.AddItem(Item);
            return RedirectToPage("Index");
        }
    }
}
