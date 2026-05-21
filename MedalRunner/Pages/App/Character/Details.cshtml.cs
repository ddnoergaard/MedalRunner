using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemModel = MedalRunner.Models.Item;

namespace MedalRunner.Pages.App.Character
{
    public class DetailsModel : PageModel
    {
        private readonly ICharacterService _characterService;

        public MedalRunner.Models.Character Character { get; set; }

        // Stores the image URL for each gear slot, populated during OnGetAsync.
        public Dictionary<ItemModel.GearSlot, string> SlotImages { get; set; } = new();

        // Stores the item ID for each gear slot, populated during OnGetAsync.
        public Dictionary<ItemModel.GearSlot, int> SlotItemIds { get; set; } = new();

        public DetailsModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Character = await _characterService.GetById(id);

            if (Character == null)
            {
                return NotFound();
            }

            // Load image and item ID for every slot up front so the view can read them directly.
            foreach (ItemModel.GearSlot slot in Enum.GetValues(typeof(ItemModel.GearSlot)))
            {
                var item = await _characterService.GetEquippedItem(Character.Id, slot);
                SlotItemIds[slot] = item != null ? item.Id : 0;
                SlotImages[slot] = item != null ? item.ImageUrl : string.Empty;
            }

            return Page();
        }

        // Returns the average item level across all equipped items, or 0 if none are equipped.
        public int GetAverageItemLevel()
        {
            if (Character.Gear == null || Character.Gear.Count == 0)
            {
                return 0;
            }
            return Character.Gear.Sum(i => i.ItemLevel) / Character.Gear.Count;
        }
    }
}
