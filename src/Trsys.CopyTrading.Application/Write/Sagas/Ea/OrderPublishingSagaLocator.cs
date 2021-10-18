using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.Ea
{
    public class OrderPublishingSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaOpenedOrderEvent> openedEvent)
            {
                return Task.FromResult<ISagaId>(new OrderPublishingSagaId("publishingopen-" + openedEvent.AggregateEvent.Order.Id.Value));
            }
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaClosedOrderEvent> closeEvent)
            {
                return Task.FromResult<ISagaId>(new OrderPublishingSagaId("publishingclose-" + closeEvent.AggregateEvent.Order.Id.Value));
            }
            throw new InvalidOperationException();
        }
    }
}