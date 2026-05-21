using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ItemModel = MedalRunner.Models.Item;

namespace MedalRunner.Pages.App.Character
{
    public class DetailsModel : PageModel
    {
        private readonly ICharacterService _characterService;
        private readonly IItemService _itemService;
        private readonly IDungeonService _dungeonService;
        public int CalcStamina { get; set; }
        public int CalcIntellect { get; set; }
        public int CalcAgility { get; set; }
        public int CalcSpirit { get; set; }
        public int CalcStrength { get; set; }
        public int CalcILevel { get; set; }
        public string specName { get; set; }
        public List<Models.Item> DisplayList { get; set; }



        public DetailsModel(ICharacterService characterService, IItemService itemService, IDungeonService dungeonService)
        {
            _characterService = characterService;
            _itemService = itemService;
            _dungeonService = dungeonService;
            DisplayList = new List<ItemModel>();
        }

        [BindProperty(SupportsGet = true)]
        public MedalRunner.Models.Character Character { get; set; }

        // Maps each gear slot int value to the equipped item (if any)
        public Dictionary<int, ItemModel> EquippedSlots { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Character = await _characterService.GetById(id);

            if (Character == null)
            {
                return NotFound();
            }
            var items = new List<ItemModel>();
            try
            {
                items = await _itemService.GetItemsByCharacterIdAsync(id);
            }catch (ArgumentException ex)
            {
                ViewData["items-not-found-msg"] = $"{ex.Message}";
            }
            foreach (var item in items)
            {
                EquippedSlots[item.Slot] = item;
                CalcStamina += item.Stamina > 0 ? item.Stamina.Value : 0;
                CalcAgility += item.Agility > 0 ? item.Agility.Value : 0;
                CalcIntellect += item.Intellect > 0 ? item.Intellect.Value : 0;
                CalcSpirit += item.Spirit > 0 ? item.Spirit.Value : 0;
                CalcStrength += item.Strength > 0 ? item.Strength.Value : 0;
                CalcILevel += item.ItemLevel > 0 ? item.ItemLevel : 0;
            }

            CalcILevel = CalcILevel / items.Count;

            try
            {
                specName = await _characterService.GetSpecNameById(Character.Id);
            } catch (SqlException)
            {
                ViewData["spec-error-msg"] = "No spec found";
            }

            if (DisplayList.Count == 0) ViewData["display-list-empty-msg"] = "Select list to get started";

            return Page();
        }

        public async Task<IActionResult> OnPostBis(int id)
        {
            DisplayList = (await _itemService.GetRandomItemsForEachSlot()).ToList();

            Character = await _characterService.GetById(id);

            if (Character == null)
            {
                return NotFound();
            }
            var items = new List<ItemModel>();
            try
            {
                items = await _itemService.GetItemsByCharacterIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                ViewData["items-not-found-msg"] = $"{ex.Message}";
            }
            foreach (var item in items)
            {
                EquippedSlots[item.Slot] = item;
                CalcStamina += item.Stamina > 0 ? item.Stamina.Value : 0;
                CalcAgility += item.Agility > 0 ? item.Agility.Value : 0;
                CalcIntellect += item.Intellect > 0 ? item.Intellect.Value : 0;
                CalcSpirit += item.Spirit > 0 ? item.Spirit.Value : 0;
                CalcStrength += item.Strength > 0 ? item.Strength.Value : 0;
                CalcILevel += item.ItemLevel > 0 ? item.ItemLevel : 0;
            }

            CalcILevel = CalcILevel / items.Count;

            try
            {
                specName = await _characterService.GetSpecNameById(Character.Id);
            }
            catch (SqlException)
            {
                ViewData["spec-error-msg"] = "No spec found";
            }

            if (DisplayList.Count == 0) ViewData["display-list-empty-msg"] = "Select list to get started";

            return Page();

        }

    }
}
