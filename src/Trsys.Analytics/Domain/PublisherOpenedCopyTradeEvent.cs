using EventFlow.Aggregates;
using System;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class PublisherOpenedCopyTradeEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherOpenedCopyTradeEvent(CopyTradeId copyTradeId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType, Price price, Lot lots)
        {
            CopyTradeId = copyTradeId;
            Timestamp = timestamp;
            Symbol = symbol;
            OrderType = orderType;
            Price = price;
            Lots = lots;
        }

        public CopyTradeId CopyTradeId { get; }
        public DateTimeOffset Timestamp { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public Price Price { get; }
        public Lot Lots { get; }
    }
}
