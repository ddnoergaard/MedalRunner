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

        public User(string firstname, string lastname, string email, string password)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;
        }

        public User()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";  
        }
    }
}
