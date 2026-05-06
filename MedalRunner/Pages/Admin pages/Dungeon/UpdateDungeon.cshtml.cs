using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin_pages.Dungeon
{
    public class UpdateDungeonModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        [BindProperty]
        public Models.Dungeon Dungeon { get; set; }

        public UpdateDungeonModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }

        public IActionResult OnGet(int id)
        {
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _dungeonService.UpdateDungeon(Dungeon);
            return Page();
        }
    }
}
