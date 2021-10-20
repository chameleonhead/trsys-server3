using EventFlow.Aggregates;

namespace Trsys.Ea.Domain
{
    public class PublisherEaClosedOrderEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaClosedOrderEvent(PublisherEaOrderEntity order)
        {
            Order = order;
        }

        public PublisherEaOrderEntity Order { get; }
    }
}