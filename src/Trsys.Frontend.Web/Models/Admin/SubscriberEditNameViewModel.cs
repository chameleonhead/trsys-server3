using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class SubscriberEditNameViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
