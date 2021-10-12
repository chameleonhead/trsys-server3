using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class SubscriberReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, AccountClientKeyUpdatedEvent>
    {
        public string Id { get; set; }
        public string ClientKey { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountClientKeyUpdatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            ClientKey = domainEvent.AggregateEvent.ClientKey.Value;
        }
    }
}
