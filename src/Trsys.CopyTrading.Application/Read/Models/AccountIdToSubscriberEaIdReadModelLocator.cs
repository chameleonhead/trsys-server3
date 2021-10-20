using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class AccountIdToSubscriberEaIdReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> registered)
            {
                yield return registered.AggregateEvent.AccountId.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent> unregistered)
            {
                yield return unregistered.AggregateEvent.AccountId.Value;
                yield break;
            }
        }
    }
}
