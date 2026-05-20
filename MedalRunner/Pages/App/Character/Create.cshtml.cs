using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedalRunner.Pages.App.Character
{
    public class CreateModel : PageModel
    {
        private readonly ICharacterService _characterService;
        private readonly IUserService _userService;

        public CreateModel(ICharacterService characterService, IUserService userService)
        {
            _characterService = characterService;
            _userService = userService;
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
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetUserByEmail(email);

            character.CreatedAt = DateTime.UtcNow;

            await _characterService.Create(character, user.Id);

            return RedirectToPage("./Index");
        }
    }
}
