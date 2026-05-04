using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedalRunner.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Slot { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public int? Intellect { get; set; }
        public int? Strenght { get; set; }
        public int? Agility { get; set; }
        public int? Spirit { get; set; }
        public int? Stamina { get; set; }
        public int? Haste { get; set; }
        public int? Crit  { get; set; }
        public int? Mastery { get; set; }
        public int? Dodge { get; set; }
        public int? Parry { get; set; }
        public double? Hit { get; set; }
        public double? Expertise { get; set; }
        public int ItemLevel { get; set; }
        public int? SocketAmount { get; set; }
        public List<string?> SocketColor { get; set; }
        public string? SocketBonus { get; set; }
        public int? Enchants { get; set; }
        public string Rarity { get; set; }
        public string Difficulty { get; set; }

        public Item()
        {
            
        }



    }
}
