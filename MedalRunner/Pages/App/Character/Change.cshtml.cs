using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App.Character
{
    public class ChangeModel : PageModel
    {
        private ICharacterService _characterService;

        public ChangeModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [BindProperty]
        public Models.Character Character { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Character = await _characterService.GetById(id);
            if (Character == null)
                return RedirectToPage("./Index");
            return Page();
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _characterService.Update(Character);
            return RedirectToPage("./Index");
        }
    }
}
