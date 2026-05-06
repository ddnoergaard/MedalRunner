using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Public_pages.Dungeons
{
    public class IndexModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        public List<Dungeon> Dungeons;
        public IndexModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }

        public async Task OnGet()
        {
            Dungeons = 
        }
    }
}
