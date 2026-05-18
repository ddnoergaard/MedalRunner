using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedalRunner.Pages
{
    public class LoginModel : PageModel
    {
        private IUserService _userService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            User user = await _userService.GetUserByEmail(Email);
            if (user.Email == Email)
            {
                var passwordHasher = new PasswordHasher<string>();

                if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, Email) };
                    
                    if ()
                }
            }
        }
    }
}
