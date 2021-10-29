using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class SubscriberEditDescriptionViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
