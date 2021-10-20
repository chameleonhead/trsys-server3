using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>,
        IEmit<CopyTradeOpenedEvent>,
        IEmit<CopyTradeApplicantAddedEvent>,
        IEmit<CopyTradeClosedEvent>,
        IEmit<CopyTradeApplicantRemovedEvent>,
        IEmit<CopyTradeFinishedEvent>
    {
        public bool IsOpen { get; private set; }
        public bool IsFinished { get; private set; }
        public List<AccountId> Subscribers { get; private set; } = new();
        public HashSet<AccountId> TradeApplicants { get; } = new();

        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }

        private void EnsureNotFinished()
        {
            if (IsNew) throw new InvalidOperationException();
            if (IsFinished) throw new InvalidOperationException();
        }

        private void EnsureIsNew()
        {
            if (!IsNew) throw new InvalidOperationException();
        }

        public void Open(DistributionGroupId distributionGroupId, PublisherId publisherId, int sequence, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers)
        {
            EnsureIsNew();
            Emit(new CopyTradeOpenedEvent(distributionGroupId, publisherId, sequence, symbol, orderType, subscribers), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void AddApplicant(AccountId accountId)
        {
            EnsureNotFinished();
            Emit(new CopyTradeApplicantAddedEvent(accountId), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void RemoveApplicant(AccountId accountId)
        {
            if (TradeApplicants.Contains(accountId))
            {
                Emit(new CopyTradeApplicantRemovedEvent(accountId), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
                if (!TradeApplicants.Any())
                {
                    Emit(new CopyTradeFinishedEvent(), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
                }
            }
        }

        public void Close(PublisherId publisherId)
        {
            EnsureNotFinished();
            if (IsOpen)
            {
                Emit(new CopyTradeClosedEvent(publisherId, Subscribers), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
                if (!TradeApplicants.Any())
                {
                    Emit(new CopyTradeFinishedEvent(), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
                }
            }
        }

        public void Apply(CopyTradeOpenedEvent aggregateEvent)
        {
            IsOpen = true;
            Subscribers = aggregateEvent.Subscribers.ToList();
        }

        public void Apply(CopyTradeApplicantAddedEvent aggregateEvent)
        {
            TradeApplicants.Add(aggregateEvent.AccountId);
        }

        public void Apply(CopyTradeClosedEvent aggregateEvent)
        {
            IsOpen = false;
        }

        public void Apply(CopyTradeApplicantRemovedEvent aggregateEvent)
        {
            TradeApplicants.Remove(aggregateEvent.AccountId);
        }

        public void Apply(CopyTradeFinishedEvent aggregateEvent)
        {
            IsFinished = true;
        }
    }
}
