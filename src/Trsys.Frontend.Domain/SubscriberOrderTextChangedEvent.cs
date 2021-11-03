using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class SubscriberOrderTextChangedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
        public SubscriberOrderTextChangedEvent(SecretKey key, EaOrderText text)
        {
            Key = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; set; }
    }
}
