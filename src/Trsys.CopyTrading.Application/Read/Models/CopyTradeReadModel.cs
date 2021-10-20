using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Linq;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class CopyTradeReadModel : IReadModel,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountTradeOrderCloseRequestDistributedEvent>
    {
        public class TradeOrderDto
        {
            public string Id { get; set; }
            public DateTimeOffset OpenDistributedTimestamp { get; set; }
            public DateTimeOffset? CloseDistributedTimestamp { get; set; }
            public bool IsOpen { get; set; }
        }

        public string DistributionGroupId { get; set; }
        public int Sequence { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public DateTimeOffset OpenPublishedTimestamp { get; set; }
        public DateTimeOffset? ClosePublishedTimestamp { get; set; }
        public List<TradeOrderDto> TradeOrders { get; } = new();
        public bool IsOpen { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent)
        {
            DistributionGroupId = domainEvent.AggregateEvent.DistributionGroupId.Value;
            Sequence = domainEvent.AggregateEvent.Sequence;
            Symbol = domainEvent.AggregateEvent.Symbol.Value;
            OrderType = domainEvent.AggregateEvent.OrderType.Value;
            OpenPublishedTimestamp = domainEvent.Timestamp;
            IsOpen = true;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent)
        {
            ClosePublishedTimestamp = domainEvent.Timestamp;
            IsOpen = false;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent> domainEvent)
        {
            var tradeOrder = TradeOrders.FirstOrDefault(e => e.Id == domainEvent.AggregateEvent.TradeOrderId.Value);
            if (tradeOrder == null)
            {
                TradeOrders.Add(new TradeOrderDto()
                {
                    Id = domainEvent.AggregateEvent.TradeOrderId.Value,
                    OpenDistributedTimestamp = domainEvent.Timestamp,
                    IsOpen = true,
                });
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderCloseRequestDistributedEvent> domainEvent)
        {
            var tradeOrder = TradeOrders.FirstOrDefault(e => e.Id == domainEvent.AggregateEvent.TradeOrderId.Value);
            if (tradeOrder != null)
            {
                tradeOrder.CloseDistributedTimestamp = domainEvent.Timestamp;
                tradeOrder.IsOpen = false;
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent> domainEvent)
        {
        }
    }
}
