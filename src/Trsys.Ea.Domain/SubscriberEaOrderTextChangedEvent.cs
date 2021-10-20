using EventFlow.Aggregates;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaOrderTextChangedEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaOrderTextChangedEvent(SecretKey key, EaOrderText text)
        {
            Key = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; set; }
    }
}
