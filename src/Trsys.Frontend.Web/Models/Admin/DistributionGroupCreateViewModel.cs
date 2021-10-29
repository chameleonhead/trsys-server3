using System.ComponentModel.DataAnnotations;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class DistributionGroupCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
