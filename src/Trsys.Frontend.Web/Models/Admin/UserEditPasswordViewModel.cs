using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class UserEditPasswordViewModel
    {
        [Required]
        public string Password { get; set; }
    }
}
