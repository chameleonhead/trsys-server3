using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Read.Models
{
    public class SubscriberIdToSubscriberEaIdReadModel : IReadModel,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent>,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent>
    {
        public string SubscriberEaId { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> domainEvent)
        {
            SubscriberEaId = domainEvent.AggregateIdentity.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
