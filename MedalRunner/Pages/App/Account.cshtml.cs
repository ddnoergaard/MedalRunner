using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedalRunner.Pages.App
{
    public class AccountModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly CookieService _cookieService;

        public AccountModel(IUserService userService, CookieService cookie)
        {
            _userService = userService;
            _cookieService = cookie;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            
        }
    }
}
