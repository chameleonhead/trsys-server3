using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeApplicantAddedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeApplicantAddedEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}