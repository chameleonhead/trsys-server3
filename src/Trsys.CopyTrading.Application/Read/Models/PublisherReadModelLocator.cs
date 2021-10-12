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
            if (domainEvent is IDomainEvent<DistributionGroupAggregate, DistributionGroupId, PublisherAddedEvent> openEvent)
            {
                yield return openEvent.AggregateEvent.ClientKey.Value;
                yield break;
            }
        }
    }
}
