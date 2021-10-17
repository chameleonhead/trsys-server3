using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class SubscriberEaReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> openEvent)
            {
                yield return openEvent.AggregateEvent.Key.Value;
                yield break; 
            }
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOrderTextUpdatedEvent> orderUpdated)
            {
                yield return orderUpdated.AggregateEvent.Key.Value;
                yield break;
            }
        }
    }
}
