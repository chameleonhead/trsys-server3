using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Sagas.Ea
{
    public class PublisherEaRegistrationSaga :
        AggregateSaga<PublisherEaRegistrationSaga, PublisherEaRegistrationSagaId, PublisherEaRegistrationSagaLocator>,
        ISagaIsStartedBy<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent>
    {
        public PublisherEaRegistrationSaga(PublisherEaRegistrationSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new DistributionGroupAddPublisherCommand(aggregateEvent.DistributionGroupId, aggregateEvent.PublisherId));
            return Task.CompletedTask;
        }
    }
}
