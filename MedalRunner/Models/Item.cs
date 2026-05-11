using System.ComponentModel.DataAnnotations;

namespace MedalRunner.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slot is required.")]
        public int Slot { get; set; }

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

        [Required(ErrorMessage = "Source is required.")]
        public string Source { get; set; } = string.Empty;

        [Required(ErrorMessage = "Armor is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Armor must be 0 or greater.")]
        public int Armor { get; set; }

        public int? MinDamage { get; set; }
        public int? MaxDamage { get; set; }
        public int? Intellect { get; set; }
        public int? Strenght { get; set; }
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
