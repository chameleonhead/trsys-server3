using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaRegisteredEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaRegisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, SubscriberId accountId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            AccountId = accountId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public SubscriberId AccountId { get; }
    }
}