using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Subscribers
{
    public class SubscriberEaRegistrationEventSubscriber :
        ISubscribeSynchronousTo<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent>
    {
        private readonly ICopyTradingService service;

        public SubscriberEaRegistrationEventSubscriber(ICopyTradingService service)
        {
            this.service = service;
        }

        public async Task HandleAsync(IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> domainEvent, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            await service.AddSubscriberAsync(aggregateEvent.DistributionGroupId.Value, aggregateEvent.SubscriberId.Value, cancellationToken);
        }
    }
}
