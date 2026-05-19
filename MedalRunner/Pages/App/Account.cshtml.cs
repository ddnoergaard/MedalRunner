using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetUserByEmail(email);

            try
            {
                await _userService.DeleteUserById(user.Id);
                await _cookieService.SignOutAsync();

                return RedirectToPage("/Index");
            }
            catch(SqlException ex)
            {
                ViewData["delete-account-msg"] = $"{ex.Message}";
            }
            return Page();
        }
    }
}
