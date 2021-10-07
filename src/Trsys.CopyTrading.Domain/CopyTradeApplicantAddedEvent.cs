using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeApplicantAddedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeApplicantAddedEvent(AccountId accountId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }
}