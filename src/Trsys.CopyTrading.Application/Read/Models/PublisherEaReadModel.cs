using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class PublisherEaReadModel : IReadModel,
        IAmReadModelFor<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string DistributorGroupId { get; set; }
        public string PublisherId { get; set; }
        public void Apply(IReadModelContext context, IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            DistributorGroupId = domainEvent.AggregateIdentity.Value;
            PublisherId = domainEvent.AggregateEvent.PublisherId.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
        }
    }
}
