using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Sagas.Ea
{
    public class PublisherEaRegistrationSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent is IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> registeredEvent)
            {
                return Task.FromResult<ISagaId>(new PublisherEaRegistrationSagaId("publisherregistration-" + registeredEvent.AggregateEvent.PublisherId.Value));
            }
            throw new InvalidOperationException();
        }
    }
}