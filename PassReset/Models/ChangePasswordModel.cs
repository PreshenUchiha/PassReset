using System.ComponentModel.DataAnnotations;

namespace PassReset.Models
{
    public class ChangePasswordModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
