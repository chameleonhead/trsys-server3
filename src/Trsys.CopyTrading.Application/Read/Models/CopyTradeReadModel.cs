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
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>
    {
        public string Id { get; set; }
        public string DistributionGroupId { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public List<string> Subscribers { get; set; }
        public bool IsOpen { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            DistributionGroupId = domainEvent.AggregateEvent.DistributionGroupId.Value;
            Symbol = domainEvent.AggregateEvent.Symbol.Value;
            OrderType = domainEvent.AggregateEvent.OrderType.Value;
            IsOpen = true;
            Subscribers = domainEvent.AggregateEvent.Subscribers.Select(e => e.Value).ToList();
        }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            IsOpen = false;
        }
    }
}
