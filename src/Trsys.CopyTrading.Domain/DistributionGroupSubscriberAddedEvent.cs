using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupSubscriberAddedEvent: AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupSubscriberAddedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}