using EventFlow.Aggregates;
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

        public void StartDistribution(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType order)
        {
            Emit(new TradeDistributionStartedEvent(copyTradeId, symbol, order, Subscriptions), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(SubscriptionAddedEvent e)
        {
            Subscriptions.Add(new Subscription(e.SubscriptionId, e.AccountId, e.Quantity));
        }

        public void Apply(TradeDistributionStartedEvent _)
        {
        }
    }
}
