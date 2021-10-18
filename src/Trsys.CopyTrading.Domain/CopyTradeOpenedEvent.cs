using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(DistributionGroupId distributionGroupId, PublisherId publisherId, int sequence, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers)
        {
            PublisherId = publisherId;
            DistributionGroupId = distributionGroupId;
            Sequence = sequence;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public PublisherId PublisherId { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public int Sequence { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<AccountId> Subscribers { get; }
    }
}