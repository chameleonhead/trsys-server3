using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupClosePublishedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupClosePublishedEvent(CopyTradeId copyTradeId, PublisherId publisherId)
        {
            CopyTradeId = copyTradeId;
            PublisherId = publisherId;
        }

        public CopyTradeId CopyTradeId { get; }
        public PublisherId PublisherId { get; }
    }
}