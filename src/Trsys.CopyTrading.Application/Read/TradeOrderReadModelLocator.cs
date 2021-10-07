using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read
{
    public class TradeOrderReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            var ev = domainEvent as IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenedEvent>;
            if (ev == null)
            {
                yield break;
            }
            yield return ev.AggregateEvent.TradeOrderId.Value;
        }
    }
}
