using EventFlow.Aggregates;
using System.Collections.Generic;
using System.Linq;
using Trsys.CopyTrading.Abstractions;

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
            Emit(new DistributionGroupOpenPublishedEvent(copyTradeId, symbol, orderType, Subscribers.ToList()), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void PublishClose(CopyTradeId copyTradeId)
        {
            Emit(new DistributionGroupClosePublishedEvent(copyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
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
        }

        public void Apply(DistributionGroupClosePublishedEvent aggregateEvent)
        {
        }
    }
}
