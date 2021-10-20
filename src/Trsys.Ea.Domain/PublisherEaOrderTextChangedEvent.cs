using EventFlow.Aggregates;

namespace Trsys.Ea.Domain
{
    public class PublisherEaOrderTextChangedEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaOrderTextChangedEvent(SecretKey key, EaOrderText text)
        {
            Key = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; }
    }
}