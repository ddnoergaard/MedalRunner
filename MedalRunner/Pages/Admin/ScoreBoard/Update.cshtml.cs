using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.ScoreBoard
{
    public class UpdateModel : PageModel
    {
        private readonly IScoreboardService _scoreboardService;

        [BindProperty]
        public Models.Scoreboard Score { get; set; }
        public UpdateModel(IScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        public async Task OnGet(int id)
        {
            Score = await _scoreboardService.GetScoreById(id);
        }
    }
}
