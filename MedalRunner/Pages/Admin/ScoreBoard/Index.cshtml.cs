using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.ScoreBoard
{
    public class IndexModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        private readonly IScoreboardService _scoreBoard;

        public List<Models.Scoreboard> Score { get; set; } = new List<Scoreboard>();
        


        public IndexModel(IScoreboardService scoreBoard, IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
            _scoreBoard = scoreBoard;
        }
        public async Task OnGet()
        {
            
            Score = await _scoreBoard.GetAllScores();
            foreach(Scoreboard score in Score)
            {
                score.Dungeon = await _dungeonService.GetDungeonByIdAsync(score.DungeonId);
            }
        }

        public IActionResult SortByNameOnPost()
        {

            return Page();
        }
        public IActionResult SortByDungeonOnPost()
        {
            return Page();
        }

        public IActionResult SortByTimeOnPost()
        {
            return Page();
        }        

        public IActionResult SortByDateOnPost()
        {
            return Page();
        }
    }
}
