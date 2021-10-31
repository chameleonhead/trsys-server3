using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
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