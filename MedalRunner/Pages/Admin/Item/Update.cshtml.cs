using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.Item
{
    public class UpdateModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly IBossService _bossService; // CHANGED: injected to look up boss by name on save

        [BindProperty]
        public Models.Item Item { get; set; }

        [BindProperty]
        public string? BossName { get; set; } // CHANGED: replaces free-text Source; pre-filled from existing drop boss

        public UpdateModel(IItemService itemService, IBossService bossService)
        {
            _itemService = itemService;
            _bossService = bossService;
        }

        // CHANGED: was blocking .Result call - now async; uses GetAllItemsWithSourceAsync to also get DropBoss for pre-fill
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var items = await _itemService.GetAllItemsWithSourceAsync();
            Item = items.FirstOrDefault(i => i.Id == id);
            if (Item == null)
            {
                return RedirectToPage("/NotFound");
            }
            BossName = Item.DropBoss; // CHANGED: pre-fills the boss name field with the current drop source
            return Page();
        }

        // CHANGED: was synchronous with no boss handling - now async, resolves boss name to BossId before saving
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // CHANGED: looks up the boss by name and sets BossId so UpdateItem can rewrite boss_drops
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

            await _itemService.UpdateItem(Item);
            return RedirectToPage("Index");
        }
    }
}
