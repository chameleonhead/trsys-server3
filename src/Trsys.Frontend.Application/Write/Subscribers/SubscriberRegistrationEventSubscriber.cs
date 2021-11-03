using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Subscribers
{
    public class SubscriberRegistrationEventSubscriber :
        ISubscribeSynchronousTo<SubscriberAggregate, SubscriberId, SubscriberRegisteredEvent>
    {
        private readonly ICopyTradingService service;

        public SubscriberRegistrationEventSubscriber(ICopyTradingService service)
        {
            this.service = service;
        }

        public async Task HandleAsync(IDomainEvent<SubscriberAggregate, SubscriberId, SubscriberRegisteredEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await service.AddSubscriberAsync(aggregateEvent.DistributionGroupId.Value, domainEvent.AggregateIdentity.Value, cancellationToken);
        }
    }
}
