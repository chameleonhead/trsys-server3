using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEaOrderTextUpdatedEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaOrderTextUpdatedEvent(SecretKey key, EaOrderText text)
        {
            Key = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; }
    }
}