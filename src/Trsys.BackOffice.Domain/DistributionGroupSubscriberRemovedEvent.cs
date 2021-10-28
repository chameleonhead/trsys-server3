using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupSubscriberRemovedEvent: AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupSubscriberRemovedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}