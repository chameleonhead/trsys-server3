using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.RegisteringEa
{
    public class SubscriberEaRegistrationSaga :
        AggregateSaga<SubscriberEaRegistrationSaga, SubscriberEaRegistrationSagaId, SubscriberEaRegistrationSagaLocator>,
        ISagaIsStartedBy<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent>
    {
        public SubscriberEaRegistrationSaga(SubscriberEaRegistrationSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new AddSubscriberCommand(aggregateEvent.DistributionGroupId, aggregateEvent.AccountId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, SubscriberAddedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
