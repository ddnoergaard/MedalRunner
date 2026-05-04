namespace MedalRunner.Models
{
    public class Scoreboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dungeon Dungeon { get; set; }
        public string Score { get; set; }

        public Scoreboard()
        {
            
        }
    }
}
