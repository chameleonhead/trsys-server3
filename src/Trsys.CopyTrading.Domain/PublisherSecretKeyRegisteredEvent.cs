using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherSecretKeyRegisteredEvent : AggregateEvent<SecretKeyAggregate, SecretKeyId>
    {
        public PublisherSecretKeyRegisteredEvent(SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId)
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