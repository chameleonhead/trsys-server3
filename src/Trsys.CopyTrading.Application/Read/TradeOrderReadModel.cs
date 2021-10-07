using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read
{
    public class TradeOrderReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderOpenedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderClosedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderInactivatedEvent>
    {
        public string AccountId { get; private set; }
        public string CopyTradeId { get; private set; }
        public string Symbol { get; private set; }
        public string OrderType { get; private set; }
        public bool IsOpen { get; private set; }
        public bool IsCloseDistributed { get; private set; }
        public bool IsOpenDistributed { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenedEvent> domainEvent)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            AccountId = domainEvent.AggregateIdentity.Value;
            CopyTradeId = aggregateEvent.CopyTradeId.Value;
            Symbol = aggregateEvent.Symbol.Value;
            OrderType = aggregateEvent.OrderType.Value;
            IsOpen = true;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent> domainEvent)
        {
            IsOpenDistributed = true;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderClosedEvent> domainEvent)
        {
            IsOpen = false;
            if (!IsOpenDistributed)
            {
                context.MarkForDeletion();
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent> domainEvent)
        {
            IsCloseDistributed = true;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderInactivatedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
