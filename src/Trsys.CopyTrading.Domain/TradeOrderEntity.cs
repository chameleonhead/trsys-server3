using EventFlow.Entities;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderEntity : Entity<TradeOrderId>
    {
        public TradeOrderEntity(TradeOrderId id, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(id)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public bool HasOpened { get; private set; }
        public bool HasClosed { get; private set; }

        public void OpenDistributed()
        {
            HasOpened = true;
        }
        public void CloseDistributed()
        {
            HasClosed = true;
        }
    }
}
