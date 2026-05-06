using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin_pages
{
    public class DeleteDungeonModel : PageModel
    {
        private readonly IDungeonService _dungeonService;

        [BindProperty]
        public Models.Dungeon Dungeon { get; set; }
        public DeleteDungeonModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var deletedItem = _dungeonService.DeleteDungeon(Dungeon.Id);
            if (deletedItem == null)
            {
                return RedirectToPage("/NotFound");
            }
            return RedirectToPage("AllDungeons");
        }
    }
}
