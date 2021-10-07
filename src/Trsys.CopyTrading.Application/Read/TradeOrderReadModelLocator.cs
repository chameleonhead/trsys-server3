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
            if (domainEvent is IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenedEvent> openEvent)
            {
                yield return openEvent.AggregateEvent.TradeOrderId.Value;
            }
            if (domainEvent is IDomainEvent<AccountAggregate, AccountId, TradeOrderClosedEvent> closeEvent)
            {
                yield return closeEvent.AggregateEvent.TradeOrderId.Value;
            }
        }
    }
}
