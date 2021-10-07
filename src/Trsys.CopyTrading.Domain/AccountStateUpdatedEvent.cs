using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountStateUpdatedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountStateUpdatedEvent(AccountBalance balance)
        {
            Balance = balance;
        }

        public AccountBalance Balance { get; }
    }
}