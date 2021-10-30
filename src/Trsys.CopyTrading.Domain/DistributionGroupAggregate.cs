using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public HashSet<AccountId> Subscribers { get; } = new();

        public void AddSubscriber(AccountId accountId)
        {
            if (!Subscribers.Contains(accountId))
            {
                Emit(new DistributionGroupSubscriberAddedEvent(accountId));
            }
        }

        public void RemvoeSubscriber(AccountId accountId)
        {
            if (Subscribers.Contains(accountId))
            {
                Emit(new DistributionGroupSubscriberRemovedEvent(accountId));
            }
        }

        public void PublishOpen(PublisherId publisherId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            Emit(new DistributionGroupOpenPublishedEvent(copyTradeId, publisherId, symbol, orderType, Subscribers.ToList()), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void PublishClose(PublisherId publisherId, CopyTradeId copyTradeId)
        {
            Emit(new DistributionGroupClosePublishedEvent(copyTradeId, publisherId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(DistributionGroupSubscriberAddedEvent aggregateEvent)
        {
            Subscribers.Add(aggregateEvent.AccountId);
        }

        public void Apply(DistributionGroupSubscriberRemovedEvent aggregateEvent)
        {
            Subscribers.Remove(aggregateEvent.AccountId);
        }

        public void Apply(DistributionGroupOpenPublishedEvent aggregateEvent)
        {
        }

        public void Apply(DistributionGroupClosePublishedEvent aggregateEvent)
        {
        }
    }
}
