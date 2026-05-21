using MedalRunner.Models;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Pages
{
    public class ScoreboardModel : PageModel
    {
        private readonly IScoreboardService _scoreboardService;
        private readonly IDungeonService _dungeonService;
        public List<Scoreboard> ScoreboardList { get; set; }
        public List<Scoreboard> DisplayScoreboardList { get; set; }
        [BindProperty]
        public string NameSearch { get; set; }
        [BindProperty]
        public string? DungeonSearch { get; set; }

        public ScoreboardModel(IScoreboardService scoreboardService, IDungeonService dungeonService)
        {
            _scoreboardService = scoreboardService;
            _dungeonService = dungeonService;
            ScoreboardList = new List<Scoreboard>();
            DisplayScoreboardList = new List<Scoreboard>();
        }

        public async Task OnGet()
        {
            try
            {
                ScoreboardList = (await _scoreboardService.GetAllScores()).ToList();
                ScoreboardList = ScoreboardList.Where(s => s.IsActive).ToList();
            } catch (SqlException)
            {
                ViewData["scoreboard-error-msg"] = "Something went wrong";
            }
            if (ScoreboardList.Count == 0) ViewData["scoreboard-error-msg"] = "No scoreboard records to show.";

            foreach (Scoreboard scoreboard in ScoreboardList)
            {
                scoreboard.Dungeon = await _dungeonService.GetDungeonByIdAsync(scoreboard.DungeonId);
            }
        }

        public async Task<IActionResult> OnPostFilter()
        {
            List<Scoreboard> scoreboards = (await _scoreboardService.GetAllScores()).ToList();

            if (string.IsNullOrEmpty(NameSearch) && string.IsNullOrEmpty(DungeonSearch))
            {
                DisplayScoreboardList.Clear();
                ScoreboardList = (await _scoreboardService.GetAllScores()).ToList();
                return Page();
            }

            if (!string.IsNullOrEmpty(NameSearch)) scoreboards = scoreboards.Where(s => s.Name.ToLower().Contains(NameSearch.ToLower())).ToList();
            if (!string.IsNullOrEmpty(DungeonSearch)) scoreboards = scoreboards.Where(s => s.DungeonId.Equals(Convert.ToInt32(DungeonSearch))).ToList();
            //if (!string.IsNullOrEmpty(IsOneHanded))
            //{
            //    if (IsOneHanded == "true")
            //    {
            //        scoreboards = scoreboards.Where(i => i.Material.StartsWith('1')).ToList();
            //    }
            //    else if (IsOneHanded == "false")
            //    {
            //        string[] tempArray = { "Cloth", "Leather", "Mail", "Plate", "Dagger", "Neck", "offh-shield", "relic", "ring", "trinket" };
            //        scoreboards = scoreboards.Where(i => !i.Material.StartsWith('1') && !tempArray.Any(t => i.Material.ToLower().Contains(t.ToLower()))).ToList();
            //    }
            //}
            DisplayScoreboardList = scoreboards;
            return Page();
        }
    }
}
