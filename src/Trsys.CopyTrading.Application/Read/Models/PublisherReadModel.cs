using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class PublisherReadModel : IReadModel,
        IAmReadModelFor<SecretKeyAggregate, SecretKeyId, PublisherSecretKeyRegisteredEvent>
    {
        public string Id { get; set; }
        public string DistributorGroupId { get; set; }
        public string Key { get; set; }
        public void Apply(IReadModelContext context, IDomainEvent<SecretKeyAggregate, SecretKeyId, PublisherSecretKeyRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateEvent.PublisherId.Value;
            DistributorGroupId = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
        }
    }
}
