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
            public string Id { get; private set; } = "N/A";

        }

        public string Id { get; private set; } = "N/A";
        public string Symbol { get; private set; } = "N/A";
        public string OrderType { get; private set; } = "N/A";
        public DateTimeOffset? OpenedAt { get; private set; }
        public DateTimeOffset? ClosedAt { get; private set; }
        public string PublisherId { get; private set; } = "N/A";
        public DateTimeOffset? PublisherOpenedAt { get; private set; }
        public decimal? PublisherPriceOpened { get; private set; }
        public decimal? PublisherLotsOpened { get; private set; }
        public DateTimeOffset? PublisherClosedAt { get; private set; }
        public string? PublisherOrderType { get; private set; }
        public decimal? PublisherPriceClosed { get; private set; }
        public decimal? PublisherLotsClosed { get; private set; }

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
            Id = domainEvent.AggregateIdentity.Value;
            ClosedAt = domainEvent.AggregateEvent.Timestamp;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherAggregate, PublisherId, PublisherOpenedCopyTradeEvent> domainEvent)
        {
            Id = domainEvent.AggregateEvent.CopyTradeId.Value;
            PublisherId = domainEvent.AggregateIdentity.Value;
            PublisherOrderType = domainEvent.AggregateEvent.OrderType.Value;
            if (!PublisherOpenedAt.HasValue || PublisherOpenedAt.Value > domainEvent.AggregateEvent.Timestamp)
            {
                PublisherOpenedAt = domainEvent.AggregateEvent.Timestamp;
            }
            if (!PublisherPriceOpened.HasValue || PublisherPriceOpened.Value > domainEvent.AggregateEvent.Price.Value)
            {
                PublisherPriceOpened = domainEvent.AggregateEvent.Price.Value;
            }
            if (!PublisherLotsOpened.HasValue)
            {
                PublisherLotsOpened = domainEvent.AggregateEvent.Lots.Value;
            }
            else
            {
                PublisherLotsOpened += domainEvent.AggregateEvent.Lots.Value;
            }
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
