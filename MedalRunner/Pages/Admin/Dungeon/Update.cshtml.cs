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

        public List<string> BossNames { get; set; }

        public UpdateDungeonModel(IDungeonService dungeonService, IBossService bossService)
        {
            _dungeonService = dungeonService;
            _bossService = bossService;
        }

        public IActionResult OnGet(int id)
        {
            Dungeon = _dungeonService.GetAllDungeons().Result.FirstOrDefault(i => i.Id == id);
            if (Dungeon == null)
            {
                return RedirectToPage("/NotFound");
            }

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

            return RedirectToPage("AllDungeons");
        }
    }
}
