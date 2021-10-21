using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class LoginReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            yield return domainEvent.Metadata["username"].ToUpperInvariant();
        }
    }
}
