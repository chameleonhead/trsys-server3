using System;
using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class PublisherAggregate : AggregateRoot<PublisherAggregate, PublisherId>,
        IEmit<PublisherOpenedCopyTradeEvent>,
        IEmit<PublisherClosedCopyTradeEvent>
    {
        public PublisherAggregate(PublisherId id) : base(id)
        {
        }

        public void Open(CopyTradeId copyTradeId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType, Price price, Lot lots)
        {
            Emit(new PublisherOpenedCopyTradeEvent(copyTradeId, timestamp, symbol, orderType, price, lots));
        }

        public void Close(CopyTradeId copyTradeId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType, Price price, Lot lots, Profit profit)
        {
            Emit(new PublisherClosedCopyTradeEvent(copyTradeId, timestamp, symbol, orderType, price, lots, profit));
        }

        public void Apply(PublisherOpenedCopyTradeEvent aggregateEvent)
        {
        }

        public void Apply(PublisherClosedCopyTradeEvent aggregateEvent)
        {
        }
    }
}