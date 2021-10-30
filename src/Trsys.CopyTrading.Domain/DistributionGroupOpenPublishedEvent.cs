using EventFlow.Aggregates;
using System.Collections.Generic;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupOpenPublishedEvent : IAggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupOpenPublishedEvent(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<SubscriberId> Subscribers { get; set; }
    }
}