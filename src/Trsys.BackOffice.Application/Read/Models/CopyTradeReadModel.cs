using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class CopyTradeReadModel : IReadModel,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>
    {
        public string Id { get; set; }
        public string DistributionGroupId { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public bool IsClosed { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            DistributionGroupId = domainEvent.AggregateEvent.DistributionGroupId.Value;
            Symbol = domainEvent.AggregateEvent.Symbol.Value;
            OrderType = domainEvent.AggregateEvent.OrderType.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent)
        {
            IsClosed = true;
        }
    }
}
