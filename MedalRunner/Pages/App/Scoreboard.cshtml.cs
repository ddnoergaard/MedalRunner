using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;

namespace MedalRunner.Pages.App
{
    public class ScoreboardModel : PageModel
    {
        private User currentUser;
        private readonly IScoreboardService _scoreboardService;
        private readonly IUserService _userService;
        private readonly IDungeonService _dungeonService;
        private readonly ICharacterService _characterService;
        public List<Scoreboard> Scoreboards { get; set; }
        public List<Models.Character> Characters { get; set; }
        [BindProperty]
        public Scoreboard NewScoreboard { get; set; }

        public ScoreboardModel(IScoreboardService scoreboardService, IUserService userService, IDungeonService dungeonService, ICharacterService characterService)
        {
            _scoreboardService = scoreboardService;
            _userService = userService;
            _dungeonService = dungeonService;
            _characterService = characterService;
            Scoreboards = new List<Scoreboard>();
            Characters = new List<Models.Character>();
            NewScoreboard = new Scoreboard();
        }

        public async Task OnGet()
        {
            currentUser = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email));
            try
            {
                Scoreboards = (await _scoreboardService.GetScoreboardRecordsByUserId(currentUser.Id)).ToList();
                Scoreboards = Scoreboards.Where(s => s.IsActive).ToList();
            } catch (ArgumentException)
            {
                ViewData["scoreboard-error-msg"] = "No records found";
            }
            
            foreach (Scoreboard scoreboard in Scoreboards)
            {
                scoreboard.Dungeon = await _dungeonService.GetDungeonByIdAsync(scoreboard.DungeonId);
            }
            Characters = (await _characterService.GetCharactersByUserId(currentUser.Id)).ToList();
        }

        public async Task<IActionResult> OnPostCreateScoreboard()
        {
            currentUser = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email));
            NewScoreboard.CreatedAt = DateTime.UtcNow;
            NewScoreboard.IsActive = true;
            NewScoreboard.UserId = currentUser.Id;
            if (NewScoreboard.RunDate is null) NewScoreboard.RunDate = DateTime.UtcNow;

            try
            {
                await _scoreboardService.CreateAsync(NewScoreboard);
                ViewData["create-success-msg"] = "Run created";
                return Page();
            } catch (SqlException)
            {
                ViewData["create-error-msg"] = "Something went wrong.";
                return Page();
            }
        }
    }
}
