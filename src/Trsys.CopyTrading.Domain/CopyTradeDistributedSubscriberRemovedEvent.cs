using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeDistributedSubscriberRemovedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeDistributedSubscriberRemovedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}