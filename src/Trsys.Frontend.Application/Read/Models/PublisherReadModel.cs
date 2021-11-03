using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using System.Linq;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Read.Models
{
    public class PublisherReadModel : IReadModel,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherRegisteredEvent>,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherUnregisteredEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public HashSet<string> DistributionGroups { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            DistributionGroups.Add(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherUnregisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            DistributionGroups.Remove(domainEvent.AggregateEvent.DistributionGroupId.Value);
            if (!DistributionGroups.Any())
            {
                context.MarkForDeletion();
            }
        }
    }
}
