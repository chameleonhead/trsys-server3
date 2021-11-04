using EventFlow.Aggregates;
using System;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType)
        {
            Timestamp = timestamp;
            Symbol = symbol;
            OrderType = orderType;
        }

        public DateTimeOffset Timestamp { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }
}
