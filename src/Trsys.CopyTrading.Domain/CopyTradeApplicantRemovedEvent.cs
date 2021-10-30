using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeApplicantRemovedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeApplicantRemovedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}