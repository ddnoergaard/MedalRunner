using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MedalRunner.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string UserLastName { get; set; }

        [BindProperty]
        public string UserEmail { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ComparePassword { get; set; }

        [BindProperty]
        public Models.User user { get; set; } = new Models.User();

        private PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (CompareAttribute.Equals(Password, ComparePassword))
            {
                user.CreatedAt = DateTime.UtcNow;
                user.RoleId = 2;
                user.SubscriptionId = 1;
                user.Email = UserEmail;
                user.FirstName = UserName;
                user.LastName = UserLastName;
                user.Password = passwordHasher.HashPassword(null, Password);
                await _userService.Create(user);
                return RedirectToPage("./Login");
            }
            else return Page();
        }
    }
}
