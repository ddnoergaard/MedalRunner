using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App.Character
{
    public class ChangeModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly ICharacterService _characterService;

        public MedalRunner.Models.Item? EquippedItem { get; set; }
        public MedalRunner.Models.Item? CompareItem { get; set; }
        public List<MedalRunner.Models.Item> SlotItems { get; set; } = new();
        public int CurrentSlot { get; set; }
        public string CurrentSlotName { get; set; } = string.Empty;
        public int RouteCharacterId { get; set; }

        [BindProperty]
        public int? SelectedItemId { get; set; }

        public ChangeModel(IItemService itemService, ICharacterService characterService)
        {
            _itemService = itemService;
            _characterService = characterService;
        }

        private async Task LoadPageData(int characterId, int slot, int itemId)
        {
            CurrentSlot = slot;
            CurrentSlotName = ((MedalRunner.Models.Item.GearSlot)slot).ToString();
            RouteCharacterId = characterId;

            if (itemId != 0)
            {
                EquippedItem = await _itemService.GetByItemId(itemId);
            }

            var allItems = await _itemService.GetAllItem();

            // Ring1 and Ring2 share the same item pool, same for Trinket1 and Trinket2.
            int lookupSlot = slot;
            if (slot == (int)MedalRunner.Models.Item.GearSlot.Ring2)
            {
                lookupSlot = (int)MedalRunner.Models.Item.GearSlot.Ring1;
            }
            if (slot == (int)MedalRunner.Models.Item.GearSlot.Trinket2)
            {
                lookupSlot = (int)MedalRunner.Models.Item.GearSlot.Trinket1;
            }

            foreach (var item in allItems)
            {
                if (item.Slot == lookupSlot && item.Id != itemId)
                {
                    SlotItems.Add(item);
                }
            }

            SlotItems = SlotItems.OrderBy(i => i.Name).ToList();
        }

        public async Task<IActionResult> OnGetAsync(int characterId, int slot, int itemId)
        {
            await LoadPageData(characterId, slot, itemId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int characterId, int slot, int itemId)
        {
            await LoadPageData(characterId, slot, itemId);

            if (SelectedItemId.HasValue)
            {
                foreach (var item in SlotItems)
                {
                    if (item.Id == SelectedItemId.Value)
                    {
                        CompareItem = item;
                        break;
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAcceptAsync(int characterId, int slot, int newItemId)
        {
            await _characterService.EquipItem(characterId, slot, newItemId);
            return RedirectToPage("/App/Character/Details", new { id = characterId });
        }

        public IActionResult OnPostCancelAsync(int characterId)
        {
            return RedirectToPage("/App/Character/Details", new { id = characterId });
        }
    }
}
