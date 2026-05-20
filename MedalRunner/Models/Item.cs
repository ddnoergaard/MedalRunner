using System.ComponentModel.DataAnnotations;

namespace MedalRunner.Models
{
    public class Item
    {
        public enum GearSlot
        {
            Tabard = 0,
            Head = 1,
            Neck = 2,
            Shoulders = 3,
            Back = 4,
            Chest = 5,
            Wrists = 6,
            Hands = 7,
            Belt = 8,
            Legs = 9,
            Feet = 10,
            Ring1 = 11,
            Ring2 = 12,
            Trinket1 = 13,
            Trinket2 = 14,
            MainHand = 15,
            OffHand = 16
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slot is required.")]
        public int Slot { get; set; }

        // Returns a display-friendly name for the slot.
        // Ring1/Ring2 both show as "Ring", Trinket1/Trinket2 as "Trinket", etc.
        // Everything else just uses the enum name directly.
        public string SlotName
        {
            get
            {
                GearSlot gearSlot = (GearSlot)Slot;

                if (gearSlot == GearSlot.Ring1 || gearSlot == GearSlot.Ring2)
                {
                    return "Ring";
                }
                else if (gearSlot == GearSlot.Trinket1 || gearSlot == GearSlot.Trinket2)
                {
                    return "Trinket";
                }
                else if (gearSlot == GearSlot.MainHand)
                {
                    return "Main-Hand";
                }
                else if (gearSlot == GearSlot.OffHand)
                {
                    return "Off-Hand";
                }
                else
                {
                    return gearSlot.ToString();
                }
            }
        }

        [Required(ErrorMessage = "Image URL is required.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Item Level is required.")]
        [Range(1, 9999, ErrorMessage = "Item Level must be between 1 and 9999.")]
        public int ItemLevel { get; set; }

        [Required(ErrorMessage = "Rarity is required.")]
        public string Rarity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Difficulty is required.")]
        public string Difficulty { get; set; } = string.Empty;

        [Required(ErrorMessage = "Material is required.")]
        public string Material { get; set; } = string.Empty;

        // Source was removed — it had no database column and was never read anywhere.
        // Drop information is loaded by GetAllItemsWithSourceAsync() via a JOIN on boss_drops.
        public string? DropBoss { get; set; }
        public string? DropDungeon { get; set; }
        public int? BossId { get; set; } // Set on POST to rewrite the boss_drops row on save

        [Required(ErrorMessage = "Armor is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Armor must be 0 or greater.")]
        public int Armor { get; set; }
        public int? MinDamage { get; set; }
        public int? MaxDamage { get; set; }
        public int? Intellect { get; set; }
        public int? Strength { get; set; }
        public int? Agility { get; set; }
        public int? Spirit { get; set; }
        public int? Stamina { get; set; }
        public int? Haste { get; set; }
        public int? Crit { get; set; }
        public int? Mastery { get; set; }
        public int? Dodge { get; set; }
        public int? Parry { get; set; }
        public int? Hit { get; set; }
        public int? Expertise { get; set; }
        public double? Speed { get; set; }
        public int? SocketAmount { get; set; }
        public string? SocketBonusStat { get; set; }
        public int? SocketBonusAmount { get; set; }
        public int? Enchants { get; set; }

        public Item()
        {
        }
    }
}
