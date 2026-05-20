using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemModel = MedalRunner.Models.Item;

namespace MedalRunner.Pages.App.Character
{
    public class DetailsModel : PageModel
    {
        private readonly ICharacterService _characterService;

        public DetailsModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

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

            // Load all items equipped by this character and map them by slot
            var items = await _characterService.GetCharacterItemsAsync(id);

            foreach (var item in items)
            {
                EquippedSlots[item.Slot] = item;
            }

            return Page();
        }
    }
}
