using MedalRunner.Models;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;

namespace MedalRunner.Pages
{
    public class LoginModel : PageModel
    {
        private IUserService _userService;

        private readonly CookieService _cookieService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(IUserService userService, CookieService cookie)
        {
            _userService = userService;
            _cookieService = cookie;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            User user = new User();
            try
            {
                user = await _userService.GetUserByEmail(Email);
            }
            catch (ArgumentException ex)
            {
                ViewData["login-error-msg"] = $"{ex.Message}";
            }

            if (user == null)
            {
                return Page();
            }

            var passwordHasher = new PasswordHasher<string>();
            try
            {
                if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                {
                    await _cookieService.SingInAsync(user);
                    if (user.RoleId == 2)
                    {
                        return RedirectToPage("/App/Index");
                    }
                    if (user.RoleId == 1)
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                } else
                {
                    ViewData["login-error-msg"] = "Invalid email or password";
                }
            }
            catch (ArgumentException ex)
            {
                ViewData["login-error-msg"] = $"{ex.Message}";
            }
            return Page();
        }

        //public async Task<IActionResult> OnPost()
        //{
        //    User user = await _userService.GetUserByEmail(Email);
        //    user.SubscriptionId = 1;
        //    if (user.Email == Email)
        //    {
        //        var passwordHasher = new PasswordHasher<string>();

        //        if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
        //        {
        //            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

        //            if (user.RoleId == 1) claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        //            var claimIdentety = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentety));

        //            return RedirectToPage("/App/Index");
        //        }
        //        Message = "Invalid email or password";
        //        return Page();
        //    }
        //    Message = "Invalid email or password";
        //    return Page();
        //}
    }
}
