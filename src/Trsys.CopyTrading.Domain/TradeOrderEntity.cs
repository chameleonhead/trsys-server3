using EventFlow.Entities;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderEntity: Entity<TradeOrderId>
    {
        public TradeOrderEntity(TradeOrderId id) : base(id)
        {
        }

        public TradeOrderEntity(TradeOrderId id, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, TradeQuantity quantity) : this(id)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
            Quantity = quantity;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public TradeQuantity Quantity { get; }
    }
}
