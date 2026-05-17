using MedalRunner.Models;
using MedalRunner.Repositories;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedalRunner.Pages.Admin_pages
{
    public class AddDungeonModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        private readonly IBossService _bossService;

        [BindProperty]
        public Models.Dungeon Dungeon { get; set; }

        [BindProperty]
        public IEnumerable<string> BossNames { get; set; } = new List<string>();

        public AddDungeonModel(IDungeonService dungeonService, IBossService bossService)
        {
            _dungeonService = dungeonService;
            _bossService = bossService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Bosses = await _bossService.GetBossesAsync();

            var foundBosses = Bosses.Where(b => BossNames.Contains(b.Name)).ToList();

            if (BossNames.Any() && foundBosses.Count != BossNames.Count())
            {
                ModelState.AddModelError("BossNames", "One or more boss names are invalid.");
                return Page();
            }

            Dungeon.Bosses = foundBosses;
            
            await _dungeonService.AddDungeon(Dungeon);
            return RedirectToPage("AllDungeons");
        }
    }
}
