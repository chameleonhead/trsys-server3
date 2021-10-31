using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupNameChangedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupNameChangedEvent(DistributionGroupName name)
        {
            Name = name;
        }

        public DistributionGroupName Name { get; }
    }
}