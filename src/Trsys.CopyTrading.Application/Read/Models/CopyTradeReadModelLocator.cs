using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class CopyTradeReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> openEvent)
            {
                yield return openEvent.AggregateIdentity.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> closeEvent)
            {
                yield return closeEvent.AggregateIdentity.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent> openDistributedEvent)
            {
                yield return openDistributedEvent.AggregateEvent.CopyTradeId.Value;
                yield break;
            }
            if (domainEvent is IDomainEvent<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent> closeDistributedEvent)
            {
                yield return closeDistributedEvent.AggregateEvent.CopyTradeId.Value;
                yield break;
            }
        }
    }
}
