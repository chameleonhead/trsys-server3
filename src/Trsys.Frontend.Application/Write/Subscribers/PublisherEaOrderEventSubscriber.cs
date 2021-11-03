using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Subscribers
{
    public class PublisherEaOrderEventSubscriber :
        ISubscribeSynchronousTo<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent>,
        ISubscribeSynchronousTo<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent>
    {
        private readonly ICopyTradingService service;

        public PublisherEaOrderEventSubscriber(ICopyTradingService service)
        {
            this.service = service;
        }

        public async Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await Task.WhenAll(aggregateEvent.Order.Targets.Select(target => service.PublishOpenTradeAsync(target.DistributionGroupId.Value, target.Id.Value, aggregateEvent.Order.Symbol.Value, aggregateEvent.Order.OrderType.Value, cancellationToken)));
        }

        public async Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await Task.WhenAll(aggregateEvent.Order.Targets.Select(target => service.PublishCloseTradeAsync(target.DistributionGroupId.Value, target.Id.Value, cancellationToken)));
        }
    }
}
