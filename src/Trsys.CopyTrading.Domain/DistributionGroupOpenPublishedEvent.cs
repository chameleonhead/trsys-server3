using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupOpenPublishedEvent : IAggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupOpenPublishedEvent(CopyTradeId copyTradeId, PublisherId publisherId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers)
        {
            CopyTradeId = copyTradeId;
            PublisherId = publisherId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public CopyTradeId CopyTradeId { get; }
        public PublisherId PublisherId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<SubscriberId> Subscribers { get; set; }
    }
}