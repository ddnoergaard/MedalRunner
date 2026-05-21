using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin_pages.Dungeon
{
    public class UpdateDungeonModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        private readonly IBossService _bossService;

        [BindProperty]
        public Models.Dungeon Dungeon { get; set; }

        // BossNames holds the five boss name inputs from the form.
        // On GET it is pre-filled from the existing dungeon bosses.
        // On POST it is validated and resolved to Boss objects before saving.
        [BindProperty]
        public List<string> BossNames { get; set; } = new();

        public UpdateDungeonModel(IDungeonService dungeonService, IBossService bossService)
        {
            _dungeonService = dungeonService;
            _bossService = bossService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Dungeon = await _dungeonService.GetDungeonByIdAsync(id);

                if (Dungeon == null)
                {
                    return RedirectToPage("/NotFound");
                }
            }
            catch (ArgumentException ex ) 
            {
                ViewData["dungeonGetId-error-msg"] = $"{ex.Message}";
            }

            try
            {
                var bosses = await _dungeonService.GetBossesAsync(id);
                BossNames = bosses.Select(b => b.Name).ToList(); // CHANGED: pre-fills the boss name inputs with current values

            }
            catch(IndexOutOfRangeException ex)
            {
                ViewData["getBossesId-error-msg"] = $"{ex.Message}";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Strip blank slots and trim whitespace from copied names.
            var filledNames = BossNames
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .ToList();

            // Reject duplicate boss names before hitting the database.
            if (filledNames.Count != filledNames.Distinct(StringComparer.OrdinalIgnoreCase).Count())
            {
                ModelState.AddModelError("BossNames", "Duplicate bosses!");
                return Page();
            }

            var uniqueNames = filledNames.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            // Resolve names to Boss objects using case-insensitive matching.
            // If any name is not found in the DB, show an error instead of saving.
            var dbBosses = await _bossService.GetBossesAsync();
            var foundBosses = dbBosses
                .Where(b => uniqueNames.Any(n => n.Equals(b.Name, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (uniqueNames.Any() && foundBosses.Count != uniqueNames.Count)
            {
                ModelState.AddModelError("BossNames", "One or more boss names were not found. Check spelling.");
                return Page();
            }

            Dungeon.Bosses = foundBosses;

            try
            {
                await _dungeonService.UpdateDungeon(Dungeon);

            }
            catch (Exception ex)
            {
                ViewData["updateDungeon-error-msg"] = $"{ex.Message}";
            }
            return RedirectToPage("/Admin/Dungeon/Index"); // CHANGED: was "AllDungeons" which doesn't exist
        }
    }
}
