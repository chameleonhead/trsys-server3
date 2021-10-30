using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaRegisteredEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaRegisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, SubscriberId subscriberId)
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