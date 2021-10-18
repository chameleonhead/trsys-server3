using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEaClosedOrderEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaClosedOrderEvent(EaOrderEntity order)
        {
            Order = order;
        }

        public EaOrderEntity Order { get; }
    }
}