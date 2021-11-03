using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherClosedOrderEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherClosedOrderEvent(PublisherOrderEntity order)
        {
            Order = order;
        }

        public PublisherOrderEntity Order { get; }
    }
}