using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Public_pages.Item
{
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;
        [BindProperty]
        public string NameSearch { get; set; }
        [BindProperty]
        public string MaterialSearch { get; set; }
        [BindProperty]
        public string IsOneHanded { get; set; }
        public List<Models.Item> _items { get; set; } = new List<Models.Item>();
        public List<Models.Item> DisplayItems { get; set; }

        public IndexModel(IItemService itemService)
        {
            _itemService = itemService;
            DisplayItems = new List<Models.Item>();
        }

        public async Task OnGet()
        {
            _items = (await _itemService.GetAllItem()).ToList();
        }

        public async Task<IActionResult> OnPostFilter()
        {
            List<Models.Item> items = (await _itemService.GetAllItem()).ToList();

            if (string.IsNullOrEmpty(NameSearch) && string.IsNullOrEmpty(MaterialSearch) && string.IsNullOrEmpty(IsOneHanded))
            {
                DisplayItems.Clear();
                _items = (await _itemService.GetAllItem()).ToList();
                return Page();
            }

            if (!string.IsNullOrEmpty(NameSearch)) items = items.Where(i => i.Name.ToLower().Contains(NameSearch.ToLower())).ToList();
            if (!string.IsNullOrEmpty(MaterialSearch)) items = items.Where(i => i.Material.ToLower().Contains(MaterialSearch.ToLower())).ToList();
            if (!string.IsNullOrEmpty(IsOneHanded))
            {
                if (IsOneHanded == "true")
                {
                    items = items.Where(i => i.Material.StartsWith('1')).ToList();
                } else if (IsOneHanded == "false")
                {
                    string[] tempArray = { "Cloth", "Leather", "Mail", "Plate", "Dagger", "Neck", "offh-shield", "relic", "ring", "trinket" };
                    items = items.Where(i => !i.Material.StartsWith('1') && !tempArray.Any(t => i.Material.ToLower().Contains(t.ToLower()))).ToList();
                }
            }
            DisplayItems = items;
            return Page();

        }
    }
}

