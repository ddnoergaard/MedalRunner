using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedalRunner.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Pages.Public_pages.Dungeons
{
    public class DetailsModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        private readonly IScoreboardService _scoreboardService;
        public Dungeon Dungeon { get; set; } = new Dungeon();
        public List<Scoreboard> ScoreboardEntries { get; set; }

        public DetailsModel(IDungeonService dungeonService, IScoreboardService scoreboardService)
        {
            _dungeonService = dungeonService;
            _scoreboardService = scoreboardService;
        }

        public async Task<IActionResult> OnGet(int id, string slug)
        {
            try
            {
                Dungeon = await _dungeonService.GetDungeonByIdAsync(id);
            } catch (SqlException ex)
            {

            }
            
            Dungeon.Bosses = (await _dungeonService.GetBossesAsync(id)).ToList();
            ScoreboardEntries = (await _scoreboardService.GetScoreboardsOnDungeonIdAsync(id)).ToList();
            return Page();
        }
    }
}
