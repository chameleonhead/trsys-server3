using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class PublisherEaUnregisteredEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaUnregisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId)
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