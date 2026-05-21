using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.Item
{
    public class UpdateModel : PageModel
    {
        private readonly IItemService _itemService;
        // IBossService added — needed to resolve the typed boss name to a BossId on save.
        private readonly IBossService _bossService;

        [BindProperty]
        public Models.Item Item { get; set; }

        // BossName replaces the old Source field. Pre-filled from DropBoss on GET,
        // resolved to a BossId on POST so UpdateItem can rewrite the boss_drops row.
        [BindProperty]
        public string? BossName { get; set; }

        public UpdateModel(IItemService itemService, IBossService bossService)
        {
            _itemService = itemService;
            _bossService = bossService;
        }

        // GetAllItemsWithSourceAsync used instead of GetById — it runs the JOIN that populates
        // DropBoss and DropDungeon, which are needed to pre-fill the boss field.
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var items = await _itemService.GetAllItemsWithSourceAsync();
            Item = items.FirstOrDefault(i => i.Id == id);
            if (Item == null)
            {
                return RedirectToPage("/NotFound");
            }
            BossName = Item.DropBoss;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // If a boss name was entered, look it up in the DB and set BossId.
            // UpdateItem will then delete the old boss_drops row and insert the new one.
            // If the name is not found, show an error instead of saving.
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
