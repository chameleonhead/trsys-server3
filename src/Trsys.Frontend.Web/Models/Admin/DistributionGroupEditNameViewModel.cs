using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class DistributionGroupEditNameViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
