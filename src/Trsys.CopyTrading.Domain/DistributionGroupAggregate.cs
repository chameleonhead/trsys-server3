﻿using EventFlow.Aggregates;
using System.Collections.Generic;
using System.Linq;
using Trsys.Core;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>,
        IEmit<DistributionGroupSubscriberAddedEvent>,
        IEmit<DistributionGroupSubscriberRemovedEvent>,
        IEmit<DistributionGroupOpenPublishedEvent>,
        IEmit<DistributionGroupClosePublishedEvent>
    {
        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }

        public HashSet<SubscriberId> Subscribers { get; } = new();
        public Dictionary<CopyTradeId, CopyTradeEntity> ActiveCopyTrades { get; } = new();

        public void AddSubscriber(SubscriberId subscriberId)
        {
            if (!Subscribers.Contains(subscriberId))
            {
                Emit(new DistributionGroupSubscriberAddedEvent(subscriberId));
            }
        }

        public void RemvoeSubscriber(SubscriberId subscriberId)
        {
            if (Subscribers.Contains(subscriberId))
            {
                Emit(new DistributionGroupSubscriberRemovedEvent(subscriberId));
            }
        }

        public void PublishOpen(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (!ActiveCopyTrades.ContainsKey(copyTradeId))
            {
                Emit(new DistributionGroupOpenPublishedEvent(copyTradeId, symbol, orderType, Subscribers.ToList()));
            }
        }

        public void PublishClose(CopyTradeId copyTradeId)
        {
            if (ActiveCopyTrades.TryGetValue(copyTradeId, out var value))
            {
                Emit(new DistributionGroupClosePublishedEvent(copyTradeId, value.Subscribers));
            }
        }

        public void Apply(DistributionGroupSubscriberAddedEvent aggregateEvent)
        {
            Subscribers.Add(aggregateEvent.SubscriberId);
        }

        public void Apply(DistributionGroupSubscriberRemovedEvent aggregateEvent)
        {
            Subscribers.Remove(aggregateEvent.SubscriberId);
        }

        public void Apply(DistributionGroupOpenPublishedEvent aggregateEvent)
        {
            ActiveCopyTrades.Add(aggregateEvent.CopyTradeId, new CopyTradeEntity(aggregateEvent.CopyTradeId, aggregateEvent.Subscribers));
        }

        public void Apply(DistributionGroupClosePublishedEvent aggregateEvent)
        {
            ActiveCopyTrades.Remove(aggregateEvent.CopyTradeId);
        }
    }
}
