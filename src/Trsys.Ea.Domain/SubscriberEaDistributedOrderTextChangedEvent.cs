using EventFlow.Aggregates;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaDistributedOrderTextChangedEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaDistributedOrderTextChangedEvent(SecretKey key, EaOrderText text)
        {
            Key  = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; }
    }
}