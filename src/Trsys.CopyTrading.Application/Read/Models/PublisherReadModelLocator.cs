using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class PublisherReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<SecretKeyAggregate, SecretKeyId, PublisherSecretKeyRegisteredEvent> openEvent)
            {
                yield return openEvent.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
