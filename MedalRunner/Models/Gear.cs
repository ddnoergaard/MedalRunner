namespace MedalRunner.Models
{
    public class Gear : Item
    {
        public string Material { get; set; }
        public int Armor { get; set; }
        public string? Effect { get; set; }

        public Gear()
        {
            
        }
    }
}
