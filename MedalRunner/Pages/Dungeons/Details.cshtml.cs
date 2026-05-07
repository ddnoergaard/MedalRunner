using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedalRunner.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Pages.Public_pages.Dungeons
{
    public class DetailsModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        public Dungeon Dungeon { get; set; }

        public DetailsModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Dungeon.Bosses = (await _dungeonService.GetBossesAsync(id)).ToList();

            return Page();
        }
    }
}
