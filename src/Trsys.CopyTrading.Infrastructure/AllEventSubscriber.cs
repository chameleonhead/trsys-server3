using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.Domain;
using Trsys.Core;

namespace Trsys.CopyTrading.Infrastructure
{
    public class AllEventSubscriber : ISubscribeSynchronousToAll
    {
        private readonly ICopyTradingEventBus eventBus;

        public AllEventSubscriber(ICopyTradingEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var e in domainEvents)
            {
                switch(e)
                {
                    case IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberAddedEvent> subAdded:
                        {
                            eventBus.Publish(new CopyTradingSubscriberAddedEvent(subAdded.AggregateIdentity.Value, subAdded.AggregateEvent.SubscriberId.Value));
                            break;
                        }
                    case IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupSubscriberRemovedEvent> subRemoved:
                        {
                            eventBus.Publish(new CopyTradingSubscriberRemovedEvent(subRemoved.AggregateIdentity.Value, subRemoved.AggregateEvent.SubscriberId.Value));
                            break;
                        }
                    case IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent> opened:
                        {
                            eventBus.Publish(new CopyTradingTradeOpenedEvent(opened.AggregateEvent.CopyTradeId.Value,  opened.AggregateIdentity.Value, opened.AggregateEvent.Symbol.Value, opened.AggregateEvent.OrderType.Value, opened.AggregateEvent.Subscribers.Select(subscriberId => subscriberId.Value).ToList()));
                            break;
                        }
                    case IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent> closed:
                        {
                            eventBus.Publish(new CopyTradingTradeClosedEvent(closed.AggregateEvent.CopyTradeId.Value, closed.AggregateEvent.Subscribers.Select(subscriberId => subscriberId.Value).ToList()));
                            break;
                        }
                }
            }
            return Task.CompletedTask;
        }
    }
}
