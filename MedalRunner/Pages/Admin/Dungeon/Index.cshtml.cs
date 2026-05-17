using MedalRunner.Services.Interfaces;
using MedalRunner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin_pages.Dungeon
{
    public class IndexModel : PageModel
    {
        private readonly IDungeonService _dungeonService;

        [BindProperty]
        public List<Models.Dungeon> Dungeons { get; set; }

        public IndexModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }
        public async Task OnGet()
        {
            Dungeons = await _dungeonService.GetAllDungeons();

            foreach (var dungeon in Dungeons)
            {
                dungeon.Bosses = await _dungeonService.GetBossesAsync(dungeon.Id);
            }
        }
    }
}
