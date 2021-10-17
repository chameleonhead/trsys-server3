using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherAddedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublisherAddedEvent(PublisherId publisherId)
        {
            PublisherId = publisherId;
        }

        public PublisherId PublisherId { get; }
    }
}