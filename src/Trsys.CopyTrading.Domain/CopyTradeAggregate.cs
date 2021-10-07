using EventFlow.Aggregates;
using System;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>
    {
        public bool IsOpen { get; private set; }

        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        public void Open(DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (!IsNew)
            {
                throw new InvalidOperationException();
            }
            Emit(new CopyTradeOpenedEvent(distributionGroupId, symbol, orderType), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void Apply(CopyTradeOpenedEvent _)
        {
            IsOpen = true;
        }
    }
}
