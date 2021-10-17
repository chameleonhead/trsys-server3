using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>,
        IEmit<PublisherAddedEvent>,
        IEmit<SubscriberAddedEvent>,
        IEmit<TradeOpenDistributionStartedEvent>,
        IEmit<TradeCloseDistributionStartedEvent>
    {
        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }

        public Dictionary<PublisherId, PublisherEntity> PublishersById { get; } = new();
        public HashSet<PublisherEntity> Publishers { get; } = new();

        public HashSet<AccountId> Subscribers { get; } = new();

        public void AddPublisher(PublisherId publisherId)
        {
            Emit(new PublisherAddedEvent(publisherId));
        }

        public void AddSubscriber(AccountId accountId)
        {
            if (Subscribers.Add(accountId))
            {
                Emit(new SubscriberAddedEvent(accountId));
            }
        }

        public void StartOpenDistribution(PublisherId publisherId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (!PublishersById.TryGetValue(publisherId, out var entity))
            {
                throw new InvalidOperationException();
            }
            Emit(new TradeOpenDistributionStartedEvent(copyTradeId, entity.Id, symbol, orderType, Subscribers.ToList()), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void StartCloseDistribution(PublisherId publisherId, CopyTradeId copyTradeId)
        {
            if (!PublishersById.TryGetValue(publisherId, out var entity))
            {
                throw new InvalidOperationException();
            }
            Emit(new TradeCloseDistributionStartedEvent(copyTradeId, entity.Id), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(PublisherAddedEvent aggregateEvent)
        {
            var entity = new PublisherEntity(aggregateEvent.PublisherId);
            Publishers.Add(entity);
            PublishersById.Add(aggregateEvent.PublisherId, entity);
        }

        public void Apply(SubscriberAddedEvent aggregateEvent)
        {
            Subscribers.Add(aggregateEvent.AccountId);
        }

        public void Apply(TradeOpenDistributionStartedEvent _)
        {
        }

        public void Apply(TradeCloseDistributionStartedEvent aggregateEvent)
        {
        }
    }
}
