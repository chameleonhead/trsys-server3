using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Read.Models
{
    public class SubscriberReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberRegisteredEvent> registered)
            {
                yield return registered.AggregateEvent.Key.Value;
                yield break; 
            }
            if (domainEvent is IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberUnregisteredEvent> unregistered)
            {
                yield return unregistered.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberOrderTextChangedEvent> orderChanged)
            {
                yield return orderChanged.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberDistributedOrderTextChangedEvent> distributedOrderCanged)
            {
                yield return distributedOrderCanged.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
