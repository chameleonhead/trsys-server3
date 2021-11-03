using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Read.Models
{
    public class SubscriberIdToSubscriberEaIdReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> registered)
            {
                yield return registered.AggregateEvent.SubscriberId.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent> unregistered)
            {
                yield return unregistered.AggregateEvent.SubscriberId.Value;
                yield break;
            }
        }
    }
}
