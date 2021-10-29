using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class PublisherEditDescriptionViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
