using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
    {
        public AccountAggregate(AccountId id) : base(id)
        {
        }
    }
}
