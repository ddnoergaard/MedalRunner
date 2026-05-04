namespace MedalRunner.Models
{
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Description { get; set; }
        public List<Boss> Bosses { get; set; }

        public Dungeon()
        {
            
        }
    }
}
