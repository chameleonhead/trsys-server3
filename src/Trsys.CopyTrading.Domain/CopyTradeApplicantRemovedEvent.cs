using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeApplicantRemovedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeApplicantRemovedEvent(AccountId accountId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }
}