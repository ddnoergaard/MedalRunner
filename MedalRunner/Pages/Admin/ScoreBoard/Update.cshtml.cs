using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Admin.ScoreBoard
{
    public class UpdateModel : PageModel
    {
        private readonly IScoreboardService _scoreboardService;
        private readonly IDungeonService _dungeonService;

        [BindProperty]
        public Models.Scoreboard Score { get; set; }

        public UpdateModel(IScoreboardService scoreboardService, IDungeonService dungeonService)
        {
            _scoreboardService = scoreboardService;
            _dungeonService = dungeonService;
        }


        public async Task OnGet(int id)
        {
            Score = await _scoreboardService.GetScoreById(id);
            
        }
        public async Task<IActionResult> OnPost()
        {
            Score.Dungeon = await _dungeonService.GetDungeonByIdAsync(Score.DungeonId);
            if (!ModelState.IsValid)
            {
               return Page();
            }
            await _scoreboardService.Update(Score);
            return RedirectToPage("/Admin/Scoreboard/Index");
        }
    }
}
