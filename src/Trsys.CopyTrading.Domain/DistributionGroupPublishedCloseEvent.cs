using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupPublishedCloseEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublishedCloseEvent(CopyTradeId copyTradeId, PublisherId publisherId)
        {
            CopyTradeId = copyTradeId;
            PublisherId = publisherId;
        }

        public CopyTradeId CopyTradeId { get; }
        public PublisherId PublisherId { get; }
    }
}