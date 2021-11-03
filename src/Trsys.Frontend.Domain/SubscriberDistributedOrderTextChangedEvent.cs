using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class SubscriberDistributedOrderTextChangedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
        public SubscriberDistributedOrderTextChangedEvent(SecretKey key, EaOrderText text)
        {
            Key  = key;
            Text = text;
        }

        public SecretKey Key { get; }
        public EaOrderText Text { get; }
    }
}