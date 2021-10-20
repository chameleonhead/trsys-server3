using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupPublishedOpenEvent : IAggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublishedOpenEvent(CopyTradeId copyTradeId, int sequence, PublisherId publisherId, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers)
        {
            CopyTradeId = copyTradeId;
            Sequence = sequence;
            PublisherId = publisherId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public CopyTradeId CopyTradeId { get; }
        public int Sequence { get; }
        public PublisherId PublisherId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<AccountId> Subscribers { get; set; }
    }
}