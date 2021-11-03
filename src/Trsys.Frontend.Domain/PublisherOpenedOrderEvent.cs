using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherOpenedOrderEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherOpenedOrderEvent(PublisherOrderEntity order)
        {
            Order = order;
        }

        public PublisherOrderEntity Order { get; }
    }
}