using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaUnregisteredEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaUnregisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, AccountId accountId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            AccountId = accountId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public AccountId AccountId { get; }
    }
}