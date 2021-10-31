using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.Domain;
using Trsys.Core;

namespace Trsys.CopyTrading.Infrastructure
{
    public class AllEventSubscriber : ISubscribeSynchronousToAll
    {
        private readonly ICopyTradingEventBus eventBus;

        public AllEventSubscriber(ICopyTradingEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var e in domainEvents)
            {
                if (e is IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> opened)
                {
                    eventBus.Publish(new CopyTradeOpened(opened.AggregateIdentity.Value, opened.AggregateEvent.DistributionGroupId.Value, opened.AggregateEvent.Symbol.Value, opened.AggregateEvent.OrderType.Value, opened.AggregateEvent.Subscribers.Select(subscriberId => subscriberId.Value).ToList()));
                }
                if (e is IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> closed)
                {
                    eventBus.Publish(new CopyTradeClosed(closed.AggregateIdentity.Value, closed.AggregateEvent.Subscribers.Select(subscriberId => subscriberId.Value).ToList()));
                }
            }
            return Task.CompletedTask;
        }
    }
}
