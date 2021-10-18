using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>,
        IEmit<CopyTradeOpenedEvent>,
        IEmit<CopyTradeApplicantAddedEvent>,
        IEmit<CopyTradeClosedEvent>
    {
        public bool IsOpen { get; private set; }
        public HashSet<AccountId> TradeApplicants { get; } = new();

        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        public void AddApplicant(AccountId accountId)
        {
            if (IsNew || !IsOpen)
            {
                throw new InvalidOperationException();
            }
            Emit(new CopyTradeApplicantAddedEvent(accountId));
        }

        public void Open(DistributionGroupId distributionGroupId, PublisherId publisherId, int sequence, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers)
        {
            if (!IsNew)
            {
                throw new InvalidOperationException();
            }
            Emit(new CopyTradeOpenedEvent(distributionGroupId, publisherId, sequence, symbol, orderType, subscribers), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void Close(PublisherId publisherId)
        {
            if (IsNew)
            {
                throw new InvalidOperationException();
            }
            if (IsOpen)
            {
                Emit(new CopyTradeClosedEvent(publisherId, TradeApplicants.ToArray()), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
            }
        }

        public void Apply(CopyTradeOpenedEvent aggregateEvent)
        {
            IsOpen = true;
        }

        public void Apply(CopyTradeApplicantAddedEvent aggregateEvent)
        {
            TradeApplicants.Add(aggregateEvent.AccountId);
        }

        public void Apply(CopyTradeClosedEvent aggregateEvent)
        {
            IsOpen = false;
        }
    }
}
