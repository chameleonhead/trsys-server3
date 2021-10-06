using EventFlow.Entities;

namespace Trsys.CopyTrading.Domain
{
    public class Subscription : Entity<SubscriptionId>
    {
        public Subscription(SubscriptionId id, AccountId accountId, TradeQuantity quantity) : base(id)
        {
            AccountId = accountId;
            Quantity = quantity;
        }

        public AccountId AccountId { get; set; }
        public TradeQuantity Quantity { get; set; }
    }
}