using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class UserEditNicknameViewModel
    {
        [Required]
        public string Nickname { get; set; }
    }
}
