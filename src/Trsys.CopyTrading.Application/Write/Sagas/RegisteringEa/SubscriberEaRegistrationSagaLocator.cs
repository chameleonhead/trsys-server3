using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.RegisteringEa
{
    public class SubscriberEaRegistrationSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent is IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> registeredEvent)
            {
                return Task.FromResult<ISagaId>(new SubscriberEaRegistrationSagaId("subscriberregistration-" + registeredEvent.AggregateEvent.AccountId.Value));
            }
            throw new InvalidOperationException();
        }
    }
}