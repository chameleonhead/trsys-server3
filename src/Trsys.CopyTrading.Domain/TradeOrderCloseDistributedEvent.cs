using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderCloseDistributedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public TradeOrderCloseDistributedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}