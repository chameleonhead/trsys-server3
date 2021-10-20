using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class AccountReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, AccountStateUpdatedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderOpenRequestedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderCloseRequestedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent>
    {
        public class TradeOrderDto
        {
            public string Id { get; set; }
            public string Symbol { get; set; }
            public string OrderType { get; set; }
        }

        public string Id { get; private set; }
        public decimal Balance { get; private set; }
        public Dictionary<string, TradeOrderDto> TradeOrdersById { get; } = new();
        public List<TradeOrderDto> TradeOrders { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountStateUpdatedEvent> domainEvent)
        {
            Balance = domainEvent.AggregateEvent.Balance.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderOpenRequestedEvent> domainEvent)
        {
            var dto = new TradeOrderDto()
            {
                Id = domainEvent.AggregateEvent.CopyTradeId.Value,
                Symbol = domainEvent.AggregateEvent.Symbol.Value,
                OrderType = domainEvent.AggregateEvent.OrderType.Value,
            };
            TradeOrders.Add(dto);
            TradeOrdersById.Add(dto.Id, dto);
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderCloseRequestedEvent> domainEvent)
        {
            if (TradeOrdersById.TryGetValue(domainEvent.AggregateEvent.CopyTradeId.Value, out var item))
            {
                TradeOrdersById.Remove(item.Id);
                TradeOrders.Remove(item);
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent> domainEvent)
        {
            if (TradeOrdersById.TryGetValue(domainEvent.AggregateEvent.CopyTradeId.Value, out var item))
            {
                TradeOrdersById.Remove(item.Id);
                TradeOrders.Remove(item);
            }
        }
    }
}
