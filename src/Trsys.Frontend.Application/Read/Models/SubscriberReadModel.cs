using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Read.Models
{
    public class SubscriberReadModel : IReadModel,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberRegisteredEvent>,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberUnregisteredEvent>,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberOrderTextChangedEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public HashSet<string> DistributionGroups { get; } = new();
        public string Text { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            DistributionGroups.Add(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberUnregisteredEvent> domainEvent)
        {
            DistributionGroups.Remove(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberOrderTextChangedEvent> domainEvent)
        {
            Text = domainEvent.AggregateEvent.Text.Value;
        }
    }
}
