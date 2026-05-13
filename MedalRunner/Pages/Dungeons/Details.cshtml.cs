using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedalRunner.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace MedalRunner.Pages.Public_pages.Dungeons
{
    public class DetailsModel : PageModel
    {
        private readonly IDungeonService _dungeonService;
        private readonly IScoreboardService _scoreboardService;
        private readonly IItemService _itemService;
        public Dungeon Dungeon { get; set; } = new Dungeon();
        public List<Scoreboard> ScoreboardEntries { get; set; } = new List<Scoreboard>();
        public List<Models.Item> Items { get; set; } = new List<Models.Item>();

        public DetailsModel(IDungeonService dungeonService, IScoreboardService scoreboardService, IItemService itemService)
        {
            _dungeonService = dungeonService;
            _scoreboardService = scoreboardService;
            _itemService = itemService;
        }

        public async Task<IActionResult> OnGet(int id, string slug)
        {
            try
            {
                Dungeon = await _dungeonService.GetDungeonByIdAsync(id);
            } catch (SqlException ex)
            {
                ViewData["dungeon-error-msg"] = $"{ex.Message}";
            }
            
            try
            {
                Dungeon.Bosses = (await _dungeonService.GetBossesAsync(id)).ToList();
            } catch (SqlException ex)
            {
                ViewData["bosses-error-msg"] = $"{ex.Message}";
            }
            
            try
            {
                ScoreboardEntries = (await _scoreboardService.GetScoreboardsOnDungeonIdAsync(id)).ToList();
            } catch (Exception ex)
            {
                ViewData["scoreboard-error-msg"] = $"{ex.Message}";
            }

            try
            {
                Items = (await _itemService.GetItemsByDungeonIdAsync(id)).ToList();
            } catch (InvalidOperationException ex)
            {
                ViewData["item-error-msg"] = $"{ex.Message}";
            }

            return Page();
        }
    }
}
