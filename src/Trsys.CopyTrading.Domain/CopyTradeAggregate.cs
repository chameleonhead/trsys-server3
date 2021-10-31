using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using Trsys.Core;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>,
        IEmit<CopyTradeOpenedEvent>,
        IEmit<CopyTradeClosedEvent>
    {
        public bool IsClosed { get; private set; }
        public List<SubscriberId> Subscribers { get; private set; } = new();

        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        private void EnsureNotNew()
        {
            if (IsNew) throw new InvalidOperationException();
        }

        private void EnsureIsNew()
        {
            if (!IsNew) throw new InvalidOperationException();
        }

        public void Open(DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers)
        {
            EnsureIsNew();
            Emit(new CopyTradeOpenedEvent(distributionGroupId, symbol, orderType, subscribers), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void Close()
        {
            EnsureNotNew();
            if (!IsClosed)
            {
                Emit(new CopyTradeClosedEvent(Subscribers), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
            }
        }

        public void Apply(CopyTradeOpenedEvent aggregateEvent)
        {
            Subscribers = aggregateEvent.Subscribers.ToList();
        }

        public void Apply(CopyTradeClosedEvent aggregateEvent)
        {
            IsClosed = true;
        }
    }
}
