using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>,
        IEmit<PublisherAddedEvent>,
        IEmit<SubscriberAddedEvent>,
        IEmit<TradeDistributionStartedEvent>
    {
        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }

        public HashSet<PublisherEntity> Publishers { get; } = new();

        public HashSet<AccountId> Subscribers { get; } = new();

        public void AddPublisher(PublisherIdentifier clientKey)
        {
            Emit(new PublisherAddedEvent(PublisherId.New, clientKey));
        }

        public void AddSubscriber(AccountId accountId)
        {
            if (Subscribers.Add(accountId))
            {
                Emit(new SubscriberAddedEvent(accountId));
            }
        }

        public void StartDistribution(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            Emit(new TradeDistributionStartedEvent(copyTradeId, symbol, orderType, Subscribers.ToList()), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(PublisherAddedEvent aggregateEvent)
        {
            Publishers.Add(new PublisherEntity(aggregateEvent.PublisherId, aggregateEvent.ClientKey));
        }

        public void Apply(SubscriberAddedEvent aggregateEvent)
        {
            Subscribers.Add(aggregateEvent.AccountId);
        }

        public void Apply(TradeDistributionStartedEvent _)
        {
        }
    }
}
