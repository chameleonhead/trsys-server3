using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using System.Linq;
using Trsys.CopyTrading.Domain;
using Trsys.Core;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class CopyTradeReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent>
    {
        public string Id { get; set; }
        public string DistributionGroupId { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public List<string> Subscribers { get; set; }
        public bool IsOpen { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent> domainEvent)
        {
            Id = domainEvent.AggregateEvent.CopyTradeId.Value;
            DistributionGroupId = domainEvent.AggregateIdentity.Value;
            Symbol = domainEvent.AggregateEvent.Symbol.Value;
            OrderType = domainEvent.AggregateEvent.OrderType.Value;
            IsOpen = true;
            Subscribers = domainEvent.AggregateEvent.Subscribers.Select(e => e.Value).ToList();
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            IsOpen = false;
        }
    }
}
