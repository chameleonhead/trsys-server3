using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupPublisherAddedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublisherAddedEvent(PublisherId publisherId)
        {
            PublisherId = publisherId;
        }

        public PublisherId PublisherId { get; }
    }
}