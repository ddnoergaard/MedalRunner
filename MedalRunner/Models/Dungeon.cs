namespace MedalRunner.Models
{
    public class Dungeon
    {
        public int Id { get; set; }
        // String defaults added — without them, nullable reference types treats unposted fields
        // as required and ModelState.IsValid silently fails on the update form.
        public string Name { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string DungeonMapUrl { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;
        public string Platinum { get; set; } = string.Empty;
        public string Gold { get; set; } = string.Empty;
        public string Silver { get; set; } = string.Empty;
        public string Bronze { get; set; } = string.Empty;
        public int MobAmount { get; set; }
        // Nullable — Bosses is not posted by the form, so it must not be treated as required.
        public IEnumerable<Boss>? Bosses { get; set; }

        public Dungeon()
        {
            
        }
    }
}
