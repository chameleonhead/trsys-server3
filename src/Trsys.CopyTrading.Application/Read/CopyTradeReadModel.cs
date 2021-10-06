using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read
{
    public class CopyTradeReadModel : IReadModel,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>
    {
        public string DistributionGroupId { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public DateTimeOffset OpenTimestamp { get; set; }
        public bool IsOpen { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent)
        {
            DistributionGroupId = domainEvent.AggregateEvent.DistributionGroupId.Value;
            Symbol = domainEvent.AggregateEvent.Symbol.Value;
            OrderType = domainEvent.AggregateEvent.OrderType.Value;
            OpenTimestamp = domainEvent.Timestamp;
            IsOpen = true;
        }
    }
}
