using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class PublisherEaUnregisteredEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaUnregisteredEvent(SecretKey key, DistributionGroupId distributionGroupId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
    }
}