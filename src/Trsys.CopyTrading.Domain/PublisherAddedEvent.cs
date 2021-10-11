using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherAddedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublisherAddedEvent(PublisherId publisherId, PublisherIdentifier clientKey)
        {
            PublisherId = publisherId;
            ClientKey = clientKey;
        }

        public PublisherId PublisherId { get; }
        public PublisherIdentifier ClientKey { get; }
    }
}