using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read
{
    public class AccountReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, AccountStateUpdatedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderOpenedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderClosedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderInactivatedEvent>
    {
        public decimal Balance { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountStateUpdatedEvent> domainEvent)
        {
            Balance = domainEvent.AggregateEvent.Balance.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenedEvent> domainEvent)
        {
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent> domainEvent)
        {
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderClosedEvent> domainEvent)
        {
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent> domainEvent)
        {
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderInactivatedEvent> domainEvent)
        {
        }
    }
}
