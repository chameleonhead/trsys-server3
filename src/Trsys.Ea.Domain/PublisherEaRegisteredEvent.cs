using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class PublisherEaRegisteredEvent : AggregateEvent<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaRegisteredEvent(SecretKey key, DistributionGroupId distributionGroupId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
    }
}