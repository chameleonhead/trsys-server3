using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class PublisherCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
