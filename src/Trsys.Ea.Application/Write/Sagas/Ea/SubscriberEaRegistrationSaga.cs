using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Sagas.Ea
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
            Publish(new DistributionGroupAddSubscriberCommand(aggregateEvent.DistributionGroupId, aggregateEvent.AccountId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
