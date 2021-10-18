using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEaOpenedOrderEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaOpenedOrderEvent(EaOrderEntity order)
        {
            Order = order;
        }

        public EaOrderEntity Order { get; }
    }
}