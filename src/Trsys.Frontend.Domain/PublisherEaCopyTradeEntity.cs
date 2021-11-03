using EventFlow.Entities;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherEaCopyTradeEntity : Entity<CopyTradeId>
    {
        public PublisherEaCopyTradeEntity(CopyTradeId id, DistributionGroupId distributionGroupId) : base(id)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }
}