using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>,
        IEmit<DistributionGroupPublisherAddedEvent>,
        IEmit<DistributionGroupSubscriberAddedEvent>,
        IEmit<DistributionGroupPublishedOpenEvent>,
        IEmit<DistributionGroupPublishedCloseEvent>
    {
        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }

        public int CurrentSequence { get; private set; }
        public Dictionary<PublisherId, PublisherEntity> PublishersById { get; } = new();
        public HashSet<PublisherEntity> Publishers { get; } = new();

        public HashSet<AccountId> Subscribers { get; } = new();

        public void AddPublisher(PublisherId publisherId)
        {
            Emit(new DistributionGroupPublisherAddedEvent(publisherId));
        }

        public void AddSubscriber(AccountId accountId)
        {
            if (Subscribers.Add(accountId))
            {
                Emit(new DistributionGroupSubscriberAddedEvent(accountId));
            }
        }

        public void PublishOpen(PublisherId publisherId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (!PublishersById.TryGetValue(publisherId, out var entity))
            {
                throw new InvalidOperationException();
            }
            Emit(new DistributionGroupPublishedOpenEvent(copyTradeId, CurrentSequence + 1, entity.Id, symbol, orderType, Subscribers.ToList()), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void PublishClose(PublisherId publisherId, CopyTradeId copyTradeId)
        {
            if (!PublishersById.TryGetValue(publisherId, out var entity))
            {
                throw new InvalidOperationException();
            }
            Emit(new DistributionGroupPublishedCloseEvent(copyTradeId, entity.Id), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(DistributionGroupPublisherAddedEvent aggregateEvent)
        {
            var entity = new PublisherEntity(aggregateEvent.PublisherId);
            Publishers.Add(entity);
            PublishersById.Add(aggregateEvent.PublisherId, entity);
        }

        public void Apply(DistributionGroupSubscriberAddedEvent aggregateEvent)
        {
            Subscribers.Add(aggregateEvent.AccountId);
        }

        public void Apply(DistributionGroupPublishedOpenEvent aggregateEvent)
        {
            CurrentSequence = aggregateEvent.Sequence;
        }

        public void Apply(DistributionGroupPublishedCloseEvent aggregateEvent)
        {
        }
    }
}
