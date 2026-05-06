using MedalRunner.Models;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin_pages
{
    public class AddDungeonModel : PageModel
    {
        private readonly IDungeonService _dungeonService;

        [BindProperty]
        public Dungeon Dungeon { get; set; }

        public AddDungeonModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _dungeonService.AddDungeon(Dungeon);
            return RedirectToPage("AllDungeons");
        }
    }
}
