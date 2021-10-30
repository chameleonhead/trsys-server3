using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeApplicantRemovedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeApplicantRemovedEvent(SubscriberId accountId)
        {
            AccountId = accountId;
        }

        public SubscriberId AccountId { get; }
    }
}