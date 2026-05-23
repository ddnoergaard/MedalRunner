using System.ComponentModel.DataAnnotations;

namespace MedalRunner.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Must insert a first name")]
        [MinLength(2, ErrorMessage = "The minimum character count is 2")]
        public string FirstName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Must insert a last name")]
        [MinLength(2, ErrorMessage = "The minimum character count is 2")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Passsword must be inserted")]
        [MinLength(3, ErrorMessage = "Password must have at least 3 characters")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email must not be empty")]
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

        }
    }
}
