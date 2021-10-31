using EventFlow;
using EventFlow.Queries;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Core;
using Trsys.Ea.Application.Read.Models;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Infrastructure
{
    public class CopyTradingEventHandler : BackgroundService
    {
        private readonly ICopyTradingEventBus eventBus;
        private readonly EaEventFlowRootResolver resolver;

        public CopyTradingEventHandler(ICopyTradingEventBus eventBus, EaEventFlowRootResolver resolver)
        {
            this.eventBus = eventBus;
            this.resolver = resolver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await eventBus.Subscribe(OnCopyTradeEvent, stoppingToken);
                }
                catch
                {
                }
            }
        }

        private async void OnCopyTradeEvent(ICopyTradingEvent copyTradingEvent)
        {
            if (copyTradingEvent is CopyTradeOpened opened)
            {
                await HandleAsync(opened, CancellationToken.None);
            }
            if (copyTradingEvent is CopyTradeClosed closed)
            {
                await HandleAsync(closed, CancellationToken.None);
            }
        }

        public async Task HandleAsync(CopyTradeOpened domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            await Task.WhenAll(domainEvent.Subscribers.Select(async subscriberId =>
            {
                var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberIdToSubscriberEaIdReadModel>(subscriberId), cancellationToken);
                if (subscriber == null)
                {
                    return;
                }
                await commandBus.PublishAsync(new SubscriberEaOpenTradeOrderCommand(SubscriberEaId.With(subscriber.SubscriberEaId), CopyTradeId.With(domainEvent.CopyTradeId), new ForexTradeSymbol(domainEvent.Symbol), OrderType.Of(domainEvent.OrderType)), cancellationToken);
            }));
        }

        public async Task HandleAsync(CopyTradeClosed domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            await Task.WhenAll(domainEvent.Subscribers.Select(async subscriberId =>
            {
                var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberIdToSubscriberEaIdReadModel>(subscriberId), cancellationToken);
                if (subscriber == null)
                {
                    return;
                }
                await commandBus.PublishAsync(new SubscriberEaCloseTradeOrderCommand(SubscriberEaId.With(subscriber.SubscriberEaId), CopyTradeId.With(domainEvent.CopyTradeId)), cancellationToken);
            }));
        }

    }
}
