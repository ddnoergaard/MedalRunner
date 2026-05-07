namespace MedalRunner.Models
{
    public class Weapon : Item
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Speed { get; set; }
        public double Dps { get; set; }
        public string GripType { get; set; }

        public Weapon()
        {
            
        }
    }
}
