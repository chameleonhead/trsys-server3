using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Subscribers
{
    public class PublisherEaOrderEventSubscriber :
        ISubscribeSynchronousTo<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent>,
        ISubscribeSynchronousTo<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent>
    {
        private readonly ICommandBus commandBus;

        public PublisherEaOrderEventSubscriber(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public async Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await Task.WhenAll(aggregateEvent.Order.Targets.Select(target => commandBus.PublishAsync(new DistributionGroupPublishOpenCommand(target.DistributionGroupId, target.Id, aggregateEvent.Order.Symbol, aggregateEvent.Order.OrderType), cancellationToken)));
        }

        public async Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await Task.WhenAll(aggregateEvent.Order.Targets.Select(target => commandBus.PublishAsync(new DistributionGroupPublishCloseCommand(target.DistributionGroupId, target.Id), cancellationToken)));
        }
    }
}
