using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Home
{
    public class LoginViewModel
    {
        public string ErrorMessage { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}