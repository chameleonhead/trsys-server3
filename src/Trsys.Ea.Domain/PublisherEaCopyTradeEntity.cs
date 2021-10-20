using EventFlow.Entities;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class PublisherEaCopyTradeEntity : Entity<CopyTradeId>
    {
        public PublisherEaCopyTradeEntity(CopyTradeId id, DistributionGroupId distributionGroupId, PublisherId publisherId) : base(id)
        {
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }
}