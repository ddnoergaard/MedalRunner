namespace MedalRunner.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; }

        public User()
        {
            
        }
    }
}
