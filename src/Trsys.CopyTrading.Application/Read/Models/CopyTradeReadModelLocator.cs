using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;
using Trsys.Core;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class CopyTradeReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            switch(domainEvent)
            {
                case IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent> opened:
                    yield return opened.AggregateEvent.CopyTradeId.Value;
                    break;
                case IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent> closed:
                    yield return closed.AggregateEvent.CopyTradeId.Value;
                    break;
            }
        }
    }
}
