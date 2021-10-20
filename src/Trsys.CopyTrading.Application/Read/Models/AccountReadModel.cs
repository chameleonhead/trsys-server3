using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class AccountReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, AccountStateUpdatedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderCloseRequestDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent>
    {
        public string Id { get; private set; }
        public decimal Balance { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountStateUpdatedEvent> domainEvent)
        {
            Balance = domainEvent.AggregateEvent.Balance.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent> domainEvent)
        {
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderCloseRequestDistributedEvent> domainEvent)
        {
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent> domainEvent)
        {
        }
    }
}
