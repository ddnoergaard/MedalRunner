using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Pages.App.Character
{
    public class EditModel : PageModel
    {
        private ICharacterService _characterService;

        public EditModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [BindProperty(SupportsGet = true)]
        public Models.Character Character { get; set; }
        private int charId;

        public async Task<IActionResult> OnGet(int id)
        {
            Character = await _characterService.GetById(id);
            charId = id;
            if (Character == null)
                return RedirectToPage("./Index");
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            //Models.Character tempCharacter = await _characterService.GetById(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Character.Id = id;
            try
            {
                await _characterService.Update(Character);
            } catch (SqlException ex)
            {
                ViewData["update-error-msg"] = $"{ex.Message}";
            }
            return RedirectToPage("./Index");
        }
    }
}
