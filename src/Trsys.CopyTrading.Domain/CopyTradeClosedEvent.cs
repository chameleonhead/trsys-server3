using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeClosedEvent(PublisherId publisherId, List<AccountId> subscribers)
        {
            PublisherId = publisherId;
            Subscribers = subscribers;
        }

        public PublisherId PublisherId { get; }
        public List<AccountId> Subscribers { get; }
    }
}
