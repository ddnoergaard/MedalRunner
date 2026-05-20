using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

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

        // CHANGED: was empty - now loads the dungeon so the confirmation page can display its name
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Dungeon = await _dungeonService.GetDungeonByIdAsync(id);

            }
            catch(ArgumentException ex)
            {
                ViewData["dungeonGet-error-msg"] = $"{ex.Message}";
            }
            if (Dungeon == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        // CHANGED: was synchronous OnPost() - now properly awaits DeleteDungeon
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _dungeonService.DeleteDungeon(Dungeon.Id);

            }
            catch (ArgumentException ex)
            {
                ViewData["dungeonDelete-error-msg"] = $"{ex.Message}";
            }
            catch (SqlException ex)
            {
                ViewData["dungeonDelete-error-msg"] = $"{ex.Message}";
            }
            return RedirectToPage("/Admin/Dungeon/Index");
        }
    }
}
