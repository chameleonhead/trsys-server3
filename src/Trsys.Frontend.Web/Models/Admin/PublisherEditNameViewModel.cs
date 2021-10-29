using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class PublisherEditNameViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
