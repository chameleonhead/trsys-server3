using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>,
        IEmit<CopyTradeOpenedEvent>,
        IEmit<CopyTradeDistributedSubscriberAddedEvent>,
        IEmit<CopyTradeClosedEvent>,
        IEmit<CopyTradeDistributedSubscriberRemovedEvent>,
        IEmit<CopyTradeFinishedEvent>
    {
        public bool IsOpen { get; private set; }
        public bool IsFinished { get; private set; }
        public List<SubscriberId> Subscribers { get; private set; } = new();
        public HashSet<SubscriberId> DistributedSubscribers { get; } = new();

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

        public void Open(DistributionGroupId distributionGroupId, PublisherId publisherId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers)
        {
            EnsureIsNew();
            Emit(new CopyTradeOpenedEvent(distributionGroupId, publisherId, symbol, orderType, subscribers), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void AddDistributedSubscriber(SubscriberId subscriberId)
        {
            EnsureNotFinished();
            Emit(new CopyTradeDistributedSubscriberAddedEvent(subscriberId), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
        }

        public void RemoveDistributedSubscriber(SubscriberId subscriberId)
        {
            if (DistributedSubscribers.Contains(subscriberId))
            {
                Emit(new CopyTradeDistributedSubscriberRemovedEvent(subscriberId), new Metadata(KeyValuePair.Create("copy-trade-id", Id.Value)));
                if (!DistributedSubscribers.Any())
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
                if (!DistributedSubscribers.Any())
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

        public void Apply(CopyTradeDistributedSubscriberAddedEvent aggregateEvent)
        {
            DistributedSubscribers.Add(aggregateEvent.SubscriberId);
        }

        public void Apply(CopyTradeClosedEvent aggregateEvent)
        {
            IsOpen = false;
        }

        public void Apply(CopyTradeDistributedSubscriberRemovedEvent aggregateEvent)
        {
            DistributedSubscribers.Remove(aggregateEvent.SubscriberId);
        }

        public void Apply(CopyTradeFinishedEvent aggregateEvent)
        {
            IsFinished = true;
        }
    }
}
