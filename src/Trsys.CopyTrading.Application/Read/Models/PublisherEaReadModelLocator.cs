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
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> openEvent)
            {
                yield return openEvent.AggregateEvent.Key.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaOrderTextUpdatedEvent> orderUpdated)
            {
                yield return orderUpdated.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
