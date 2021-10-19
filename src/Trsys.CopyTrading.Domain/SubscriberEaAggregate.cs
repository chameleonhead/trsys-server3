using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class SubscriberEaAggregate : AggregateRoot<SubscriberEaAggregate, SubscriberEaId>,
        IEmit<SubscriberEaRegisteredEvent>,
        IEmit<SubscriberEaUnregisteredEvent>,
        IEmit<SubscriberEaOrderTextUpdatedEvent>
    {
        public EaOrderText Text { get; private set; }
        public SecretKey Key { get; private set; }

        public SubscriberEaAggregate(SubscriberEaId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId, AccountId accountId)
        {
            Emit(new SubscriberEaRegisteredEvent(key, distributionGroupId, accountId));
        }

        public void Unregister(DistributionGroupId distributionGroupId, AccountId accountId)
        {
            Emit(new SubscriberEaUnregisteredEvent(Key, distributionGroupId, accountId));
        }

        public void UpdateOrderText(EaOrderText text)
        {
            if (Text != text)
            {
                Emit(new SubscriberEaOrderTextUpdatedEvent(Key, text));
            }
        }

        public void Apply(SubscriberEaRegisteredEvent aggregateEvent)
        {
            Key = aggregateEvent.Key;
        }

        public void Apply(SubscriberEaUnregisteredEvent aggregateEvent)
        {
        }

        public void Apply(SubscriberEaOrderTextUpdatedEvent aggregateEvent)
        {
            Text = aggregateEvent.Text;
        }
    }
}
