using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using MedalRunner.Models;
using System.Threading.Tasks;

namespace MedalRunner.Pages.Public_pages.Dungeons
{
    public class IndexModel : PageModel
    {
        private IDungeonService _dungeonService;
        public List<Dungeon> Dungeons { get; set; } = new List<Dungeon>();
        public List<Dungeon> DisplayDungeons { get; set; } = new List<Dungeon>();
        [BindProperty]
        public string NameSearch { get; set; }
        [BindProperty]
        public string ZoneSearch { get; set; }
        [BindProperty]
        public string BossSearch { get; set; }
        

        public IndexModel(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }
        public async Task OnGet()
        {
            Dungeons = await _dungeonService.GetAllDungeons();
            foreach (Dungeon dungeon in Dungeons)
            {
                dungeon.Bosses = (await _dungeonService.GetBossesAsync(dungeon.Id)).ToList();
            }
        }

        public async Task<IActionResult> OnPostFilter()
        {
            List<Dungeon> tempDungeons = (await _dungeonService.GetAllDungeons()).ToList();

            foreach (Dungeon dungeon in tempDungeons)
            {
                dungeon.Bosses = (await _dungeonService.GetBossesAsync(dungeon.Id)).ToList();
            }

            if (string.IsNullOrEmpty(NameSearch) && string.IsNullOrEmpty(ZoneSearch) && string.IsNullOrEmpty(BossSearch))
            {
                DisplayDungeons.Clear();
                Dungeons = (await _dungeonService.GetAllDungeons()).ToList();
                foreach (Dungeon dungeon in Dungeons)
                {
                    dungeon.Bosses = (await _dungeonService.GetBossesAsync(dungeon.Id)).ToList();
                }
                return Page();
            }

            

            if (!string.IsNullOrEmpty(NameSearch)) tempDungeons = tempDungeons.Where(i => i.Name.ToLower().Contains(NameSearch.ToLower())).ToList();
            if (!string.IsNullOrEmpty(ZoneSearch)) tempDungeons = tempDungeons.Where(i => i.Zone.ToLower().Contains(ZoneSearch.ToLower())).ToList();
            if (!string.IsNullOrEmpty(BossSearch))tempDungeons = tempDungeons.Where(i => i.Bosses.Any(b => b.Name.ToLower().Contains(BossSearch.ToLower()))).ToList();

            DisplayDungeons = tempDungeons;
            return Page();

        }
    }
}
