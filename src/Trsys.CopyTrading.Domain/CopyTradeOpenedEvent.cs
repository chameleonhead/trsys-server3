using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(ForexTradeSymbol symbol, OrderType orderType)
        {
            Symbol = symbol;
            OrderType = orderType;
        }

        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }
}