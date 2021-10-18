using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.Ea
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
                Publish(new PublishOrderOpenCommand(target.DistributionGroupId, target.PublisherId, target.Id, aggregateEvent.Order.Symbol, aggregateEvent.Order.OrderType));
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            foreach (var target in aggregateEvent.Order.Targets)
            {
                Publish(new PublishOrderCloseCommand(target.DistributionGroupId, target.PublisherId, target.Id));
            }
            return Task.CompletedTask;
        }
    }
}
