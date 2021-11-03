using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Frontend.Abstractions;

namespace Trsys.Frontend.Infrastructure
{
    public class AllEventSubscriber : ISubscribeSynchronousToAll
    {
        private readonly IFrontendEventBus eventBus;

        public AllEventSubscriber(IFrontendEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
