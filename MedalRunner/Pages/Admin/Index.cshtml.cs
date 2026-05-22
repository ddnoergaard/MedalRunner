using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MedalRunner.Pages.Admin_pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ICharacterService _characterService;
        private readonly IItemService _itemService;
        private readonly IScoreboardService _scoreboardService;
        private readonly IDungeonService _dungeonService;
        public int userCount { get; set; }
        public int characterCount { get; set; }
        public int itemCount { get; set; }
        public int scoreboardCount { get; set; }
        public List<Scoreboard> Scoreboards { get; set; }
        public List<User> Users { get; set; }
        public User currentUser { get; set; }
        public DateOnly DateOnly { get; set; }

        public IndexModel(IUserService userService, ICharacterService characterService, IItemService itemService, IScoreboardService scoreboardService, IDungeonService dungeonService)
        {
            _userService = userService;
            _characterService = characterService;
            _itemService = itemService;
            _scoreboardService = scoreboardService;
            _dungeonService = dungeonService;
            Scoreboards = new List<Scoreboard>();
            Users = new List<User>();
        }

        public async Task OnGet()
        {
            userCount = await _userService.GetUserCount();
            characterCount = await _characterService.GetCharacterCount();
            itemCount = await _itemService.GetItemCount();
            scoreboardCount = await _scoreboardService.GetScoreboardCount();
            Scoreboards = (await _scoreboardService.GetFiveLatestScoreboards()).ToList();
            Scoreboards.ForEach(async s => s.Dungeon = await _dungeonService.GetDungeonByIdAsync(s.DungeonId));
            Users = (await _userService.GetFiveLatestUsers()).ToList();
            currentUser = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email));
            DateOnly = DateOnly.FromDateTime(DateTime.UtcNow);
        }
    }
}
