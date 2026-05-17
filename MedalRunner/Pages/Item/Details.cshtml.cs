using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.Public_pages.Item
{
    public class DetailsModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly IBossService _bossService;
        public Models.Item Item { get; set; }
        public bool IsWeapon { get; set; } = new bool();
        public string SlotName { get; set; }
        public decimal? DPS { get; set; }
        public string WeaponHandAmount { get; set; }
        public string? WeaponType { get; set; } = null;
        public string TitleColor { get; set; }
        public List<Boss> BossList = new List<Boss>();

        public DetailsModel(IItemService itemService, IBossService bossService)
        {
            _itemService = itemService;
            _bossService = bossService;
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
                if (Item.Material.Contains("-"))
                {
                    String[] tempArray = Item.Material.Split("-");
                    if (tempArray[0].Contains("1h")) WeaponHandAmount = "One-hand";
                    if (tempArray[0].Contains("2h")) WeaponHandAmount = "Two-hand";
                    if (tempArray[0].Contains("offh")) WeaponHandAmount = "Off-hand";
                    if (tempArray.Length > 1)
                    {
                        if (tempArray[1].Contains("mace")) WeaponType = "Mace";
                        if (tempArray[1].Contains("polearm") && WeaponType is null) WeaponType = "Polearm";
                        if (tempArray[1].Contains("axe") && WeaponType is null) WeaponType = "Axe";
                        if (tempArray[1].Contains("shield") && WeaponType is null) WeaponType = "Shield";
                        if (tempArray[1].Contains("sword") && WeaponType is null) WeaponType = "Sword";
                        if (tempArray[1].Contains("staff") && WeaponType is null) WeaponType = "Staff";
                    }
                }

                if (Item.Material == "staff")
                {
                    WeaponType = "Staff";
                    WeaponHandAmount = "Two-hand";
                }

                if (Item.Material == "bow")
                {
                    WeaponType = "Bow";
                    WeaponHandAmount = "Two-hand";
                }
                
                if (WeaponType is null)
                {
                    WeaponType = "Dagger";
                    WeaponHandAmount = "One-hand";
                }
            }

            TitleColor = TitleColorCheck(Item.Rarity);

            BossList = (await _bossService.GetBossesByItemId(id)).ToList();

        }
    }
}

