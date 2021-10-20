using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEaOpenedOrderEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaOpenedOrderEvent(PublisherEaOrderEntity order)
        {
            Order = order;
        }

        public PublisherEaOrderEntity Order { get; }
    }
}