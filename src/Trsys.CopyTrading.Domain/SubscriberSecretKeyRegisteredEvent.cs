using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class SubscriberSecretKeyRegisteredEvent : AggregateEvent<SecretKeyAggregate, SecretKeyId>
    {
        public SubscriberSecretKeyRegisteredEvent(SecretKey key, AccountId accountId)
        {
            Key = key;
            AccountId = accountId;
        }

        public SecretKey Key { get; }
        public AccountId AccountId { get; }
    }
}