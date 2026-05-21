using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemModel = MedalRunner.Models.Item;

namespace MedalRunner.Pages.App.Character
{
    public class DetailsModel : PageModel
    {
        private readonly ICharacterService _characterService;
        private readonly IItemService _itemService;

        public MedalRunner.Models.Character Character { get; set; }

        // All items currently equipped by this character.
        public List<ItemModel> EquippedItems { get; set; } = new();

        public DetailsModel(ICharacterService characterService, IItemService itemService)
        {
            _characterService = characterService;
            _itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Character = await _characterService.GetById(id);

            if (Character == null)
            {
                return NotFound();
            }

            List<ItemModel> items = new List<ItemModel>();
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
                EquippedItems.Add(item);
            }

            return Page();
        }

        // Returns the item ID for a given slot, or 0 if nothing is equipped there.
        public int GetSlotItemId(ItemModel.GearSlot slot)
        {
            var item = EquippedItems.FirstOrDefault(i => i.Slot == (int)slot);
            if (item != null)
            {
                return item.Id;
            }
            return 0;
        }

        // Returns the image URL for a given slot.
        // If an item is equipped there, use its image. Otherwise use the slot placeholder.
        public string GetSlotImageUrl(ItemModel.GearSlot slot)
        {
            var item = EquippedItems.FirstOrDefault(i => i.Slot == (int)slot);
            if (item != null)
            {
                return item.ImageUrl;
            }
            return ItemModel.PlaceholderImage((int)slot);
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
