using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using System.Linq;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberRemovedEvent>
    {
        public string Id { get; private set; }
        public List<string> Subscribers { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Subscribers.Add(domainEvent.AggregateEvent.SubscriberId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberRemovedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Subscribers.Remove(domainEvent.AggregateEvent.SubscriberId.Value);
            if (!Subscribers.Any())
            {
                context.MarkForDeletion();
            }
        }
    }
}
