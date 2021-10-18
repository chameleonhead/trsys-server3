using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, PublisherAddedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, SubscriberAddedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, TradeOpenDistributionStartedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, TradeCloseDistributionStartedEvent>
    {
        public class CopyTradeDto
        {
            public string Id { get; set; }
            public string Symbol { get; set; }
            public string OrderType { get; set; }
            public int Sequence { get; set; }
        }

        public Dictionary<string, CopyTradeDto> CopyTradesById { get; } = new();
        public List<CopyTradeDto> CopyTrades { get; } = new();
        public HashSet<string> Publishers { get; } = new();
        public HashSet<string> Subscribers { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, PublisherAddedEvent> domainEvent)
        {
            Publishers.Add(domainEvent.AggregateEvent.PublisherId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, SubscriberAddedEvent> domainEvent)
        {
            Subscribers.Add(domainEvent.AggregateEvent.AccountId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, TradeOpenDistributionStartedEvent> domainEvent)
        {
            var dto = new CopyTradeDto()
            {
                Id = domainEvent.AggregateEvent.CopyTradeId.Value,
                Sequence = domainEvent.AggregateEvent.Sequence,
                Symbol = domainEvent.AggregateEvent.Symbol.Value,
                OrderType = domainEvent.AggregateEvent.OrderType.Value,
            };
            CopyTrades.Add(dto);
            CopyTradesById.Add(dto.Id, dto);
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, TradeCloseDistributionStartedEvent> domainEvent)
        {
            if (CopyTradesById.TryGetValue(domainEvent.AggregateEvent.CopyTradeId.Value, out var item))
            {
                CopyTradesById.Remove(item.Id);
                CopyTrades.Remove(item);
            }
        }
    }
}
