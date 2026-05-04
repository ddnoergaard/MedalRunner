namespace MedalRunner.Models
{
    public class Boss
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Drops { get; set; }

        public Boss()
        {

        }
    }
}
