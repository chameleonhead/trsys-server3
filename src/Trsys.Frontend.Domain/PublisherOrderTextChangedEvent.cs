using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherOrderTextChangedEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherOrderTextChangedEvent(SecretKey key, EaOrderText text)
        {
            Key = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; }
    }
}