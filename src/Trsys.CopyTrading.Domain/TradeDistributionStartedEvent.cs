using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOpenDistributionStartedEvent : IAggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public TradeOpenDistributionStartedEvent(CopyTradeId copyTradeId, PublisherId publisherId, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers)
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
        public List<AccountId> Subscribers { get; set; }
    }
}