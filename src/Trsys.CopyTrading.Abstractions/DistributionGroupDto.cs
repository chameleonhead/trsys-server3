using System.Collections.Generic;

namespace Trsys.CopyTrading.Abstractions
{
    public class DistributionGroupDto
    {
        public string Id { get; set; }
        public List<string> Subscribers { get; set; }
    }
}