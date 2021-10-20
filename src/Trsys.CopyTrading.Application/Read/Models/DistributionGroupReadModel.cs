using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupPublisherAddedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent>
    {
        public HashSet<string> Publishers { get; } = new();
        public HashSet<string> Subscribers { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupPublisherAddedEvent> domainEvent)
        {
            Publishers.Add(domainEvent.AggregateEvent.PublisherId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent> domainEvent)
        {
            Subscribers.Add(domainEvent.AggregateEvent.AccountId.Value);
        }
    }
}
