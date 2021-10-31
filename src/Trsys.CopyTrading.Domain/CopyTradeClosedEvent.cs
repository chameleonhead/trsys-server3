using EventFlow.Aggregates;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeClosedEvent(List<SubscriberId> subscribers)
        {
            Subscribers = subscribers;
        }

        public List<SubscriberId> Subscribers { get; }
    }
}
