using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class LoginReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<UserAggregate, UserId, UserUsernameChangedEvent> usernameChanged)
            {
                yield return usernameChanged.AggregateEvent.Username.Value.ToUpperInvariant();
            }
            if (domainEvent is IDomainEvent<UserAggregate, UserId, UserDeletedEvent> deletedEvent)
            {
                yield return deletedEvent.AggregateEvent.Username.Value.ToUpperInvariant();
            }
        }
    }
}
