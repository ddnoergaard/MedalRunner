using MedalRunner.Services;
using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedalRunner.Pages.App.Character
{
    public class IndexModel : PageModel
    {
        private readonly ICharacterService _characterService;
        private readonly IUserService _userService;
        public List<Models.Character> Characters { get; set; } = new List<Models.Character>();
        private User currentUser;

        public IndexModel(ICharacterService characterService, IUserService userService)
        {
            _characterService = characterService;
            _userService = userService;
        }

        public async Task OnGet()
        {
            currentUser = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email));
            Characters = (await _characterService.GetCharactersByUserId(currentUser.Id)).ToList();
            
        }
    }
}
