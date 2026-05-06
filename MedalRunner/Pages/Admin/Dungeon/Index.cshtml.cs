using MedalRunner.Services.Interfaces;
using MedalRunner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin_pages.Dungeon
{
    public class AllDungeonsModel : PageModel
    {
        public readonly IDungeonService _dungeonService;

        public AllDungeonsModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }

        public void OnGet()
        {

        }
    }
}
