using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Read.Models
{
    public class SubscriberEaReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> registered)
            {
                yield return registered.AggregateEvent.Key.Value;
                yield break; 
            }
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent> unregistered)
            {
                yield return unregistered.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOrderTextChangedEvent> orderChanged)
            {
                yield return orderChanged.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaDistributedOrderTextChangedEvent> distributedOrderCanged)
            {
                yield return distributedOrderCanged.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
