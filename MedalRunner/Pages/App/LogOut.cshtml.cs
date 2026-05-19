using MedalRunner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App
{
    public class LogOutModel : PageModel
    {
        private readonly CookieService _cookieService;

        public LogOutModel(CookieService cookie)
        {
            _cookieService = cookie;
        }

        public async Task<IActionResult> OnGet()
        {
            await _cookieService.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
