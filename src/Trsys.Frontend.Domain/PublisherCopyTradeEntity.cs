using EventFlow.Entities;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherCopyTradeEntity : Entity<CopyTradeId>
    {
        public PublisherCopyTradeEntity(CopyTradeId id, DistributionGroupId distributionGroupId) : base(id)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }
}