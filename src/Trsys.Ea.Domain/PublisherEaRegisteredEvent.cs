using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class PublisherEaRegisteredEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaRegisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }
}