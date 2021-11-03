using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Subscribers
{
    public class PublisherOrderEventSubscriber :
        ISubscribeSynchronousTo<PublisherAggregate, PublisherId, PublisherOpenedOrderEvent>,
        ISubscribeSynchronousTo<PublisherAggregate, PublisherId, PublisherClosedOrderEvent>
    {
        private readonly ICopyTradingService service;

        public PublisherOrderEventSubscriber(ICopyTradingService service)
        {
            this.service = service;
        }

        public async Task HandleAsync(IDomainEvent<PublisherAggregate, PublisherId, PublisherOpenedOrderEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await Task.WhenAll(aggregateEvent.Order.Targets.Select(target => service.PublishOpenTradeAsync(target.DistributionGroupId.Value, target.Id.Value, aggregateEvent.Order.Symbol.Value, aggregateEvent.Order.OrderType.Value, cancellationToken)));
        }

        public async Task HandleAsync(IDomainEvent<PublisherAggregate, PublisherId, PublisherClosedOrderEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await Task.WhenAll(aggregateEvent.Order.Targets.Select(target => service.PublishCloseTradeAsync(target.DistributionGroupId.Value, target.Id.Value, cancellationToken)));
        }
    }
}
