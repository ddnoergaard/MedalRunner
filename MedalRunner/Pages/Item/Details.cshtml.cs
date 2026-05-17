using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Public_pages.Item
{
    public class DetailsModel : PageModel
    {
        private readonly IItemService _itemService;
        public Models.Item Item { get; set; }
        public bool IsWeapon { get; set; } = new bool();
        public string SlotName { get; set; }
        public decimal? DPS { get; set; }
        public string WeaponHandAmount { get; set; }
        public string WeaponType { get; set; }
        public string TitleColor { get; set; }

        public DetailsModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        private bool IsWeaponCheck(int id)
        {
            List<int> weaponInts = new List<int> { 15, 16 };
            if (weaponInts.Contains(id)) {
                return true;
            }
            return false;
        }

        private string TitleColorCheck(string rarity)
        {
            if (Item.Rarity == "Common") return "white";
            if (Item.Rarity == "Uncommon") return "#1BE900";
            if (Item.Rarity == "Rare") return "#0A6CDD";
            if (Item.Rarity == "Epic") return "#A32AA2";
            if (Item.Rarity == "Legendary") return "#FF7E09";
            if (Item.Rarity == "Heirloom") return "#937F48";
            return "white";
        }

        public async Task OnGet(int id)
        {
            try
            {
                Item = await _itemService.GetByItemId(id);
            } catch (Exception ex)
            {
                ViewData["item-error-msg"] = $"{ex.Message}";
            }
            
            IsWeapon = IsWeaponCheck(Item.Slot);

            try
            {
                SlotName = await _itemService.GetItemSlotNameAsync(Item.Slot);
            } catch (ArgumentException ex)
            {
                ViewData["slot-name-error-msg"] = $"{ex.Message}";
            }

            if (IsWeapon)
            {
                if (Item.Speed == 0) Item.Speed = 1.90;
                DPS = Math.Round(Convert.ToDecimal(((Item.MinDamage + Item.MaxDamage) / 2) / Item.Speed), 2);
                String[] tempList = Item.Material.Split("-");
                if (tempList[0].Contains("1h")) WeaponHandAmount = "One-hand";
                if (tempList[0].Contains("2h")) WeaponHandAmount = "Two-hand";
                if (tempList[0].Contains("offh")) WeaponHandAmount = "Off-hand";
                if (tempList[1].Contains("mace")) WeaponType = "Mace";
                if (tempList[1].Contains("polearm")) WeaponType = "Polearm";
                if (tempList[1].Contains("axe")) WeaponType = "Axe";
                if (tempList[1].Contains("shield")) WeaponType = "Shield";
                if (tempList[1].Contains("sword")) WeaponType = "Sword";
                if (WeaponType is null)
                {
                    WeaponType = "Dagger";
                    WeaponHandAmount = "One-hand";
                }
            }

            TitleColor = TitleColorCheck(Item.Rarity);

        }
    }
}

