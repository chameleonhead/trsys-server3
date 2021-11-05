using EventFlow.Aggregates;
using System;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class PublisherClosedCopyTradeEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherClosedCopyTradeEvent(CopyTradeId copyTradeId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType, Price price, Lot lots, Profit profit)
        {
            CopyTradeId = copyTradeId;
            Timestamp = timestamp;
            Symbol = symbol;
            OrderType = orderType;
            Price = price;
            Lots = lots;
            Profit = profit;
        }

        public CopyTradeId CopyTradeId { get; }
        public DateTimeOffset Timestamp { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public Price Price { get; }
        public Lot Lots { get; }
        public Profit Profit { get; }
    }
}
