using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedalRunner.Pages.App
{
    public class IndexModel : PageModel
    {
        public string Name { get; set; }
        public DateOnly TodayDate { get; set; }
        public void OnGet()
        {
            TodayDate = DateOnly.FromDateTime(DateTime.Now);
            Name = User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
