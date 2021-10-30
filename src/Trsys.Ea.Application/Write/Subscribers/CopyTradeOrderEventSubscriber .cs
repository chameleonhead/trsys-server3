using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Application.Read.Models;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Subscribers
{
    public class CopyTradeOrderEventSubscriber :
        ISubscribeSynchronousTo<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>,
        ISubscribeSynchronousTo<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public CopyTradeOrderEventSubscriber(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        public async Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent, CancellationToken cancellationToken)
        {
            await Task.WhenAll(domainEvent.AggregateEvent.Subscribers.Select(async subscriberId =>
            {
                var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberIdToSubscriberEaIdReadModel>(subscriberId.Value), cancellationToken);
                if (subscriber == null)
                {
                    return;
                }
                await commandBus.PublishAsync(new SubscriberEaOpenTradeOrderCommand(SubscriberEaId.With(subscriber.SubscriberEaId), domainEvent.AggregateIdentity, domainEvent.AggregateEvent.Symbol, domainEvent.AggregateEvent.OrderType), cancellationToken);
            }));
        }

        public async Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent, CancellationToken cancellationToken)
        {
            await Task.WhenAll(domainEvent.AggregateEvent.Subscribers.Select(async subscriberId =>
            {
                var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberIdToSubscriberEaIdReadModel>(subscriberId.Value), cancellationToken);
                if (subscriber == null)
                {
                    return;
                }
                await commandBus.PublishAsync(new SubscriberEaCloseTradeOrderCommand(SubscriberEaId.With(subscriber.SubscriberEaId), domainEvent.AggregateIdentity), cancellationToken);
            }));
        }
    }
}
