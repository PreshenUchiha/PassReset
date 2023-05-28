using System.ComponentModel.DataAnnotations;

namespace PassReset.Models
{

    public class SignupModel
    {
        [Required]
        
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
