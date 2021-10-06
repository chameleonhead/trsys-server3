using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class SubscriptionAddedEvent: AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {

        public SubscriptionAddedEvent(SubscriptionId subscriptionId, AccountId accountId, TradeQuantity quantity)
        {
            SubscriptionId = subscriptionId;
            AccountId = accountId;
            Quantity = quantity;
        }

        public SubscriptionId SubscriptionId { get; }
        public AccountId AccountId { get; }
        public TradeQuantity Quantity { get; }
    }
}