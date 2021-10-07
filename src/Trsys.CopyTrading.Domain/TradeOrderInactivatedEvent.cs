using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderInactivatedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public TradeOrderInactivatedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}