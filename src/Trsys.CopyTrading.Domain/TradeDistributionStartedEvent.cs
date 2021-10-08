using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class TradeDistributionStartedEvent : IAggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public TradeDistributionStartedEvent(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<AccountId> Subscribers { get; set; }
    }
}