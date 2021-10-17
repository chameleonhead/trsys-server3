using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class SecretKeyAggregate : AggregateRoot<SecretKeyAggregate, SecretKeyId>,
        IEmit<PublisherSecretKeyRegisteredEvent>,
        IEmit<SubscriberSecretKeyRegisteredEvent>
    {
        public SecretKeyAggregate(SecretKeyId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId)
        {
            Emit(new PublisherSecretKeyRegisteredEvent(key, distributionGroupId, publisherId));
        }

        public void Register(SecretKey key, AccountId accountId)
        {
            Emit(new SubscriberSecretKeyRegisteredEvent(key, accountId));
        }

        public void Apply(PublisherSecretKeyRegisteredEvent aggregateEvent)
        {
        }

        public void Apply(SubscriberSecretKeyRegisteredEvent aggregateEvent)
        {
        }
    }
}
