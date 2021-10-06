﻿using EventFlow.Aggregates;
using System;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>
    {
        public bool IsOpen { get; private set; }

        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        public void Open(ForexTradeSymbol symbol, OrderType orderType)
        {
            if (!IsNew)
            {
                throw new InvalidOperationException();
            }
            Emit(new CopyTradeOpenedEvent(symbol, orderType));
        }

        public void Apply(CopyTradeOpenedEvent _)
        {
            IsOpen = true;
        }
    }
}
