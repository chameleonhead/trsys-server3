using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, TradeOpenDistributionStartedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, TradeCloseDistributionStartedEvent>
    {
        public class CopyTradeDto
        {
            public string Id { get; set; }
            public string Symbol { get; set; }
            public string OrderType { get; set; }
        }

        public Dictionary<string, CopyTradeDto> CopyTradesById { get; } = new();
        public List<CopyTradeDto> CopyTrades { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, TradeOpenDistributionStartedEvent> domainEvent)
        {
            CopyTrades.Add(new CopyTradeDto()
            {
                Id = domainEvent.AggregateEvent.CopyTradeId.Value,
                Symbol = domainEvent.AggregateEvent.Symbol.Value,
                OrderType = domainEvent.AggregateEvent.OrderType.Value,
            });
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
