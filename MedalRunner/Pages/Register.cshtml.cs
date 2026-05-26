using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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

        //[BindProperty]
        public Models.User user { get; set; } = new Models.User();

        private PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public void OnGet()
        {
        }

        private bool ContainsEmoji(params string[] inputs)
        {
            foreach (var input in inputs)
            {
                if (string.IsNullOrEmpty(input)) continue;

                for (int i = 0; i < input.Length; i++)
                {
                    int codePoint = char.IsHighSurrogate(input[i]) && i + 1 < input.Length && char.IsLowSurrogate(input[i + 1])
                        ? char.ConvertToUtf32(input[i], input[i + 1])
                        : input[i];

                    if (IsEmojiCodePoint(codePoint))
                        return true;

                    if (char.IsHighSurrogate(input[i]))
                        i++;
                }
            }

            return false;
        }

        private bool IsEmojiCodePoint(int cp)
        {
            return
                (cp >= 0x1F600 && cp <= 0x1F64F) ||
                (cp >= 0x1F300 && cp <= 0x1F5FF) ||
                (cp >= 0x1F680 && cp <= 0x1F6FF) ||
                (cp >= 0x1F900 && cp <= 0x1F9FF) ||
                (cp >= 0x2600 && cp <= 0x26FF) ||
                (cp >= 0x2700 && cp <= 0x27BF) ||
                (cp >= 0xFE00 && cp <= 0xFE0F) ||
                (cp >= 0x1FA00 && cp <= 0x1FA6F) ||
                (cp >= 0x1FA70 && cp <= 0x1FAFF) ||
                cp == 0x200D;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ContainsEmoji(UserName, UserLastName, UserEmail, Password, ComparePassword))
            {
                ViewData["emoji-error"] = "Can't use emojis. Try again.";
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
                try
                {
                    await _userService.Create(user);
                    return RedirectToPage("./Login");
                } catch (Exception ex)
                {
                    ViewData["error-msg"] = $"{ex.Message}";
                    return Page();
                }
                
            }
            else
            {
                ViewData["error-msg"] = "Can not register. Try again";
                return Page();
            }
        }
    }
}
