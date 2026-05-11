namespace MedalRunner.Models
{
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string DungeonMapUrl { get; set; }
        public string Platinum { get; set; }
        public string Gold { get; set; }
        public string Silver { get; set; }
        public string Bronze { get; set; }
        public int MobAmount { get; set; }
        public IEnumerable<Boss> Bosses { get; set; }

        public Dungeon()
        {
            
        }
    }
}
