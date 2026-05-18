using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App.Character
{
    public class CreateModel : PageModel
    {
        private readonly ICharacterService _characterService;
        
        public CreateModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [BindProperty]
        public Models.Character character { get; set; } = new Models.Character();

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            character.CreatedAt = DateTime.UtcNow;

            await _characterService.Create(character);

            return RedirectToPage("./Index");
        }
    }
}
