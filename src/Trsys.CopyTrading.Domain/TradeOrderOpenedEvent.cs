using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderOpenedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public TradeOrderOpenedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, TradeQuantity quantity)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
            Quantity = quantity;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public TradeQuantity Quantity { get; }
    }
}