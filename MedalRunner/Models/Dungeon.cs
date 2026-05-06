namespace MedalRunner.Models
{
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Description { get; set; }
        public bool Platinum { get; set; }
        public bool Gold { get; set; }
        public bool Silver { get; set; }
        public bool Bronze { get; set; }
        public int MopAmount { get; set; }
        public List<Boss> Bosses { get; set; }

        public Dungeon()
        {
            
        }
    }
}
