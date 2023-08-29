
using System.ComponentModel.DataAnnotations;

namespace Job_Search_App.ViewModels

{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
