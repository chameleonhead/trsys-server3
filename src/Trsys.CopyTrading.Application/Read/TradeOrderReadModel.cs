using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read
{
    public class TradeOrderReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, TradeOrderOpenedEvent>
    {
        public string AccountId { get; private set; }
        public string CopyTradeId { get; private set; }
        public string Symbol { get; private set; }
        public string OrderType { get; private set; }
        public bool IsOpen { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenedEvent> domainEvent)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            AccountId = domainEvent.AggregateIdentity.Value;
            CopyTradeId = aggregateEvent.CopyTradeId.Value;
            Symbol = aggregateEvent.Symbol.Value;
            OrderType = aggregateEvent.OrderType.Value;
            IsOpen = true;
        }
    }
}
