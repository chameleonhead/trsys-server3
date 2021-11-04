using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using Trsys.Analytics.Domain;
using Trsys.Core;

namespace Trsys.Analytics.Application.Read.Models
{
    public class CopyTradeReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            switch(domainEvent)
            {
                case IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> e:
                    yield return e.AggregateIdentity.Value;
                    break;
                case IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> e:
                    yield return e.AggregateIdentity.Value;
                    break;
                case IDomainEvent<PublisherAggregate, PublisherId, PublisherOpenedCopyTradeEvent> e:
                    yield return e.AggregateEvent.CopyTradeId.Value;
                    break;
                case IDomainEvent<PublisherAggregate, PublisherId, PublisherClosedCopyTradeEvent> e:
                    yield return e.AggregateEvent.CopyTradeId.Value;
                    break;
                case IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberOpenedCopyTradeEvent> e:
                    yield return e.AggregateEvent.CopyTradeId.Value;
                    break;
                case IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberClosedCopyTradeEvent> e:
                    yield return e.AggregateEvent.CopyTradeId.Value;
                    break;
            }
        }
    }
}
