using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class SubscriberReadModel : IReadModel,
        IAmReadModelFor<SecretKeyAggregate, SecretKeyId, SubscriberSecretKeyRegisteredEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<SecretKeyAggregate, SecretKeyId, SubscriberSecretKeyRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
        }
    }
}
