using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>
    {
        public bool IsOpen { get; private set; }
        public HashSet<AccountId> TradeApplicants { get; } = new();

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

        public void AddApplicant(AccountId accountId)
        {
            if (IsNew || !IsOpen)
            {
                throw new InvalidOperationException();
            }
            Emit(new CopyTradeApplicantAddedEvent(accountId));
        }

        public void Apply(CopyTradeOpenedEvent _)
        {
            IsOpen = true;
        }

        public void Apply(CopyTradeApplicantAddedEvent e)
        {
            TradeApplicants.Add(e.AccountId);
        }
    }
}
