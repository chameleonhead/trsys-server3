using EventFlow.Aggregates;
using System;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>,
        IEmit<CopyTradeOpenedEvent>,
        IEmit<CopyTradeClosedEvent>
    {
        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        public void Open(DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType)
        {
            Emit(new CopyTradeOpenedEvent(timestamp, symbol, orderType));
        }

        public void Close(DateTimeOffset timestamp)
        {
            Emit(new CopyTradeClosedEvent(timestamp));
        }

        public void Apply(CopyTradeOpenedEvent aggregateEvent)
        {
        }

        public void Apply(CopyTradeClosedEvent aggregateEvent)
        {
        }
    }
}