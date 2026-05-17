using MedalRunner.Services;
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

        [BindProperty] // CHANGED: was missing [BindProperty] so boss names typed in the form were never received on POST
        public List<string> BossNames { get; set; } = new();

        public UpdateDungeonModel(IDungeonService dungeonService, IBossService bossService)
        {
            _dungeonService = dungeonService;
            _bossService = bossService;
        }

        // CHANGED: was blocking .Result - now async; also pre-fills BossNames from existing dungeon bosses
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Dungeon = await _dungeonService.GetDungeonByIdAsync(id);
            if (Dungeon == null)
            {
                return RedirectToPage("/NotFound");
            }
            var bosses = await _dungeonService.GetBossesAsync(id);
            BossNames = bosses.Select(b => b.Name).ToList(); // CHANGED: pre-fills the boss name inputs with current values
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbBosses = await _bossService.GetBossesAsync();

            var foundBosses = dbBosses.Where(b => BossNames.Contains(b.Name)).ToList();

            if (BossNames.Any() && foundBosses.Count != BossNames.Count())
            {
                ModelState.AddModelError("BossNames", "One or more boss names are invalid.");
                return Page();
            }

            Dungeon.Bosses = foundBosses;

            await _dungeonService.UpdateDungeon(Dungeon);

            return RedirectToPage("/Admin/Dungeon/Index"); // CHANGED: was "AllDungeons" which doesn't exist
        }
    }
}
