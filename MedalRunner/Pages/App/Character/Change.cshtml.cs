using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App.Character
{
    public class ChangeModel : PageModel
    {
        private readonly IItemService _itemService;

        // The item currently equipped in the slot, passed in via route
        public MedalRunner.Models.Item EquippedItem { get; set; }

        // Items available in the same slot to compare against
        public List<MedalRunner.Models.Item> SlotItems { get; set; } = new();

        // The item selected from the list for comparison; null until the user picks one
        public MedalRunner.Models.Item? CompareItem { get; set; }

        // The id of the item the user selected from the list on POST
        [BindProperty]
        public int? SelectedItemId { get; set; }

        public ChangeModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int itemId)
        {
            EquippedItem = await _itemService.GetByItemId(itemId);
            if (EquippedItem == null)
                return RedirectToPage("/NotFound");

            // Load only items that share the same gear slot
            SlotItems = (await _itemService.GetAllItem()).Where(i => i.Slot == EquippedItem.Slot && i.Id != EquippedItem.Id).OrderBy(i => i.Name).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int itemId)
        {
            EquippedItem = await _itemService.GetByItemId(itemId);
            if (EquippedItem == null)
                return RedirectToPage("/NotFound");

            var all = await _itemService.GetAllItem();
            SlotItems = all.Where(i => i.Slot == EquippedItem.Slot && i.Id != EquippedItem.Id).OrderBy(i => i.Name).ToList();

            // Load the selected comparison item if one was chosen
            if (SelectedItemId.HasValue)
                CompareItem = SlotItems.FirstOrDefault(i => i.Id == SelectedItemId.Value);

            return Page();
        }
    }
}
