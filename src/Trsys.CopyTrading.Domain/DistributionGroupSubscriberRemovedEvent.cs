using EventFlow.Aggregates;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupSubscriberRemovedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupSubscriberRemovedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}