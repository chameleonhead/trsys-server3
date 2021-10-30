using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Read.Models
{
    public class PublisherEaReadModel : IReadModel,
        IAmReadModelFor<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent>,
        IAmReadModelFor<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisteredEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
        }
    }
}
