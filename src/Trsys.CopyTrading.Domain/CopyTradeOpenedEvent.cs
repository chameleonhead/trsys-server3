using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(PublisherId publisherId, DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType, System.Collections.Generic.List<AccountId> subscribers)
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
        public List<AccountId> Subscribers { get; }
    }
}