using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class SubscriberCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
