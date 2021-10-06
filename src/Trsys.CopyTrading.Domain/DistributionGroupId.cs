using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupId : Identity<DistributionGroupId>
    {
        public DistributionGroupId(string value) : base(value)
        {
        }
    }
}