using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeDistributedSubscriberAddedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeDistributedSubscriberAddedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}