using EventFlow.Aggregates;
using System;

namespace Trsys.BackOffice.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>,
        IEmit<CopyTradeOpenedEvent>,
        IEmit<CopyTradeClosedEvent>
    {
        public DistributionGroupId DistributionGroupId { get; private set; }
        public CopyTradeSymbol Symbol { get; private set; }
        public CopyTradeOrderType OrderType { get; private set; }
        public bool IsClosed { get; private set; }

        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        private void EnsureNotClosed()
        {
            if (IsClosed)
            {
                throw new InvalidOperationException();
            }
        }

        public void Open(DistributionGroupId distributionGroupId, CopyTradeSymbol symbol, CopyTradeOrderType orderType)
        {
            EnsureNotClosed();
            Emit(new CopyTradeOpenedEvent(distributionGroupId, symbol, orderType));
        }

        public void Close()
        {
            Emit(new CopyTradeClosedEvent());
        }

        public void Apply(CopyTradeOpenedEvent aggregateEvent)
        {
            DistributionGroupId = aggregateEvent.DistributionGroupId;
            Symbol = aggregateEvent.Symbol;
            OrderType = aggregateEvent.OrderType;
        }

        public void Apply(CopyTradeClosedEvent aggregateEvent)
        {
            IsClosed = true;
        }
    }
}