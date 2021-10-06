using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Emit(new CopyTradeOpenedEvent(distributionGroupId, symbol, orderType));
        }

        public void Apply(CopyTradeOpenedEvent _)
        {
            IsOpen = true;
        }
    }
}
