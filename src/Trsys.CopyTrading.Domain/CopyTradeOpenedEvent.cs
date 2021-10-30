using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers)
        {
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<SubscriberId> Subscribers { get; }
    }
}