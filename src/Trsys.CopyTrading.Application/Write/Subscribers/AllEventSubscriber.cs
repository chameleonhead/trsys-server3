using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading.Application.Write.Subscribers
{
    public class AllEventSubscriber : ISubscribeSynchronousToAll
    {
        private readonly AllEventBus allEventBus;

        public AllEventSubscriber(AllEventBus allEventBus)
        {
            this.allEventBus = allEventBus;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            allEventBus.Events.Add(domainEvents);
            return Task.CompletedTask;
        }
    }
}
