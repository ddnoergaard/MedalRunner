using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.ScoreBoard
{
    public class IndexModel : PageModel
    {
        private readonly IScoreboardService _scoreBoard;

        public List<Scoreboard> Scoreboard { get; set; }

        public IndexModel(IScoreboardService scoreBoard)
        {
            _scoreBoard = scoreBoard;
        }
        public void OnGet()
        {
            Scoreboard = _scoreBoard.GetAllScores();
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
