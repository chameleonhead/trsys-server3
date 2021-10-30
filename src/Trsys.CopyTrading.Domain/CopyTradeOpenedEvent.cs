using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(DistributionGroupId distributionGroupId, PublisherId publisherId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers)
        {
            PublisherId = publisherId;
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public PublisherId PublisherId { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<SubscriberId> Subscribers { get; }
    }
}