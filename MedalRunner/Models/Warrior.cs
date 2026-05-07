namespace MedalRunner.Models
{
    public class Warrior : CharacterClass
    {
        public int Rage { get; set; }
        public string Stance { get; set; }
        public bool ShieldEquipped { get; set; }

        public Warrior()
        {
            
        }
    }
}
