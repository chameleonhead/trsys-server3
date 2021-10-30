using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Sagas.Ea
{
    public class OrderPublishingSaga :
        AggregateSaga<OrderPublishingSaga, OrderPublishingSagaId, OrderPublishingSagaLocator>,
        ISagaIsStartedBy<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent>,
        ISagaIsStartedBy<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent>
    {
        public OrderPublishingSaga(OrderPublishingSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            foreach (var target in aggregateEvent.Order.Targets)
            {
                Publish(new DistributionGroupPublishOpenCommand(target.DistributionGroupId, target.Id, aggregateEvent.Order.Symbol, aggregateEvent.Order.OrderType));
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            foreach (var target in aggregateEvent.Order.Targets)
            {
                Publish(new DistributionGroupPublishCloseCommand(target.DistributionGroupId, target.Id));
            }
            return Task.CompletedTask;
        }
    }
}
