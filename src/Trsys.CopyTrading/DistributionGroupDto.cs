using System.Collections.Generic;

namespace Trsys.CopyTrading
{
    public class DistributionGroupDto
    {
        public string Id { get; internal set; }
        public List<string> Subscribers { get; internal set; }
    }
}