using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupDisplayNameChangedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupDisplayNameChangedEvent(DistributionGroupDisplayName displayName)
        {
            DisplayName = displayName;
        }

        public DistributionGroupDisplayName DisplayName { get; }
    }
}