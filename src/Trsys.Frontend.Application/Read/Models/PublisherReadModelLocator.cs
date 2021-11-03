using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Read.Models
{
    public class PublisherReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<PublisherAggregate, PublisherId, PublisherRegisteredEvent> registered)
            {
                yield return registered.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<PublisherAggregate, PublisherId, PublisherUnregisteredEvent> unregistered)
            {
                yield return unregistered.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<PublisherAggregate, PublisherId, PublisherOrderTextChangedEvent> orderChanged)
            {
                yield return orderChanged.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
