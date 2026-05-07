using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App.Character
{
    public class DeleteModel : PageModel
    {
        private ICharacterService _characterService;

        public DeleteModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [BindProperty]
        public Models.Character Character { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Character = await _characterService.GetById(id);
            if (Character == null)
                return RedirectToPage("");

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await _characterService.Delete(Character.Id);
            return RedirectToPage("./Index");
        }
    }
}
