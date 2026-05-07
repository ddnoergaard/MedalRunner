using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedalRunner.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slot { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int ItemLevel { get; set; }
        public string Rarity { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty; // Source to see where it's from.
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
        public int? Speed { get; set; }
        public int? SocketAmount { get; set; }
        public string? SocketBonusStat { get; set; }
        public int? SocketBonusAmount { get; set; }
        public string? Enchants { get; set; }


        public Item()
        {
            
        }



    }
}
