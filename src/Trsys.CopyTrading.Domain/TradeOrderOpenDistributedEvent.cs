using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderOpenDistributedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public TradeOrderOpenDistributedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}