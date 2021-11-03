using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherUnregisteredEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherUnregisteredEvent(SecretKey key, DistributionGroupId distributionGroupId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
    }
}