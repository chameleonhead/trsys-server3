using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class TradeCloseDistributionStartedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public TradeCloseDistributionStartedEvent(CopyTradeId copyTradeId, PublisherId publisherId)
        {
            CopyTradeId = copyTradeId;
            PublisherId = publisherId;
        }

        public CopyTradeId CopyTradeId { get; }
        public PublisherId PublisherId { get; }
    }
}