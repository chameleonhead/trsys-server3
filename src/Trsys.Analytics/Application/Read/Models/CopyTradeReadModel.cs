using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using Trsys.Analytics.Domain;
using Trsys.Core;

namespace Trsys.Analytics.Application.Read.Models
{
    public class CopyTradeReadModel : IReadModel,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>,
        IAmReadModelFor<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherOpenedCopyTradeEvent>,
        IAmReadModelFor<PublisherAggregate, PublisherId, PublisherClosedCopyTradeEvent>,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberOpenedCopyTradeEvent>,
        IAmReadModelFor<SubscriberAggregate, SubscriberId, SubscriberClosedCopyTradeEvent>
    {
        public class SubscriberTradeDto
        {
            public string Id { get; set; } = "N/A";

        }

        public string Id { get; set; } = "N/A";
        public string Symbol { get; set; } = "N/A";
        public string OrderType { get; set; } = "N/A";
        public DateTimeOffset? OpenedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }
        public DateTimeOffset? PublisherOpenedAt { get; set; }
        public DateTimeOffset? PublisherClosedAt { get; set; }
        public Dictionary<string, SubscriberTradeDto> SubscriberTradeResults { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Symbol = domainEvent.AggregateEvent.Symbol.Value;
            OrderType = domainEvent.AggregateEvent.OrderType.Value;
            OpenedAt = domainEvent.AggregateEvent.Timestamp;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherOpenedCopyTradeEvent> domainEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherClosedCopyTradeEvent> domainEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberOpenedCopyTradeEvent> domainEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberClosedCopyTradeEvent> domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
