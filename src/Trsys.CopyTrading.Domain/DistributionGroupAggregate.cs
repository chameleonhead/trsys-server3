using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }
    }
}
