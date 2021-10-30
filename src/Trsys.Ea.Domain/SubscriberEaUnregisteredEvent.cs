using EventFlow.Aggregates;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaUnregisteredEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaUnregisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, SubscriberId subscriberId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            SubscriberId = subscriberId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public SubscriberId SubscriberId { get; }
    }
}