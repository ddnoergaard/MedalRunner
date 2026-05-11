namespace MedalRunner.Models
{
    public class Scoreboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DungeonId { get; set; }
        public Dungeon Dungeon { get; set; }
        public string Score { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public Scoreboard()
        {
            
        }
    }
}
