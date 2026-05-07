using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using MedalRunner.Models;
using System.Threading.Tasks;

namespace MedalRunner.Pages.Public_pages.Dungeons
{
    public class IndexModel : PageModel
    {
        private IDungeonService _dungeonService;
        public List<Dungeon> _dungeons;
        public List<Dungeon> _displayDungeons;
        public List<Boss> _bosses;

        public IndexModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }
        public async Task OnGet()
        {
            _dungeons = await _dungeonService.GetAllDungeons();
        }
    }
}
