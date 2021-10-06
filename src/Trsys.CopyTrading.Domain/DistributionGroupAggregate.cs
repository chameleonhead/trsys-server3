using EventFlow.Aggregates;
using System;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }

        public List<Subscription> Subscriptions { get; private set; } = new();

        public void AddSubscriber(AccountId accountId, TradeQuantity quantity)
        {
            Emit(new SubscriptionAddedEvent(SubscriptionId.New, accountId, quantity));
        }

        public void Apply(SubscriptionAddedEvent e)
        {
            Subscriptions.Add(new Subscription(e.SubscriptionId, e.AccountId, e.Quantity));
        }
    }
}
