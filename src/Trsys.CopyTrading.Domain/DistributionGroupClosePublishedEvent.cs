using EventFlow.Aggregates;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupClosePublishedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupClosePublishedEvent(CopyTradeId copyTradeId, List<SubscriberId> subscribers)
        {
            CopyTradeId = copyTradeId;
            Subscribers = subscribers;
        }

        public CopyTradeId CopyTradeId { get; }
        public List<SubscriberId> Subscribers { get; }
    }
}