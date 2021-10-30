using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent>
    {
        public string Id { get; private set; }
        public HashSet<string> Subscribers { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Subscribers.Add(domainEvent.AggregateEvent.AccountId.Value);
        }
    }
}
