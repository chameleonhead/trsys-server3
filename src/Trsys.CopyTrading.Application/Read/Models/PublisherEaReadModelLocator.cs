using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class PublisherEaReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> registered)
            {
                yield return registered.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisteredEvent> unregistered)
            {
                yield return unregistered.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaOrderTextChangedEvent> orderChanged)
            {
                yield return orderChanged.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
