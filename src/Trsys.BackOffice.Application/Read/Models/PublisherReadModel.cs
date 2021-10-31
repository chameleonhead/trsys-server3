using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class PublisherReadModel : IReadModel,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherNameChangedEvent>,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherDescriptionChangedEvent>,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherDeletedEvent>
    {
        public string Id { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherNameChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Name = domainEvent.AggregateEvent.Name.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherDescriptionChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Description = domainEvent.AggregateEvent.Description.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherDeletedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
