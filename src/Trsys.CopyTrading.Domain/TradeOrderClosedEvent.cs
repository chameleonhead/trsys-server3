using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderClosedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public TradeOrderClosedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}