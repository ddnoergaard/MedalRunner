namespace MedalRunner.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public List<Item> Weapon { get; set; }
        public List<Item> Gear { get; set; }
        public int Specialization { get; set; }
        public DateTime CreatedAt { get; set; }

        public Character()
        {
            
        }
    }
}
