using EventFlow.Aggregates;
using System;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class PublisherOpenedCopyTradeEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherOpenedCopyTradeEvent(CopyTradeId copyTradeId, DateTimeOffset timestamp, Price price, Lot lots)
        {
            CopyTradeId = copyTradeId;
            Timestamp = timestamp;
            Price = price;
            Lots = lots;
        }

        public CopyTradeId CopyTradeId { get; }
        public DateTimeOffset Timestamp { get; }
        public Price Price { get; }
        public Lot Lots { get; }
    }
}
