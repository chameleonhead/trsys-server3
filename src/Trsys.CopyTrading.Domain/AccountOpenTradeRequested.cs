using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountTradeOrderOpenRequestedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountTradeOrderOpenRequestedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }
}
