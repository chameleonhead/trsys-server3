using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class TradeDistributionStartedEvent : IAggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public TradeDistributionStartedEvent(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType order, List<Subscription> subscriptions)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = order;
            Subscriptions = subscriptions;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<Subscription> Subscriptions { get; set; }
    }
}