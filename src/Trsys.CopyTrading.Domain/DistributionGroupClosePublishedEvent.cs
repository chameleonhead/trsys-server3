using EventFlow.Aggregates;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupClosePublishedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupClosePublishedEvent(CopyTradeId copyTradeId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }
}