using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherAddedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublisherAddedEvent(PublisherId publisherId, ClientKey clientKey)
        {
            PublisherId = publisherId;
            ClientKey = clientKey;
        }

        public PublisherId PublisherId { get; }
        public ClientKey ClientKey { get; }
    }
}