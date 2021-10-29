using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class SubscriberReadModel : IReadModel,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberNameChangedEvent>,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberDescriptionChangedEvent>,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberDeletedEvent>
    {
        public string Id { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberNameChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Name = domainEvent.AggregateEvent.Name.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberDescriptionChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Description = domainEvent.AggregateEvent.Description.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberDeletedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
