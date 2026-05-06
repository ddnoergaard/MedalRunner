namespace MedalRunner.Models
{
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Platinum { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public int MopAmount { get; set; }


        public Dungeon()
        {
            
        }
    }
}
