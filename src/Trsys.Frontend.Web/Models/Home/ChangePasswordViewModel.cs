using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Home
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPasswordConfirm { get; set; }
    }
}
