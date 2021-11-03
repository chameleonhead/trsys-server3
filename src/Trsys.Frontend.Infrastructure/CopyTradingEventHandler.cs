using EventFlow;
using EventFlow.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Core;
using Trsys.Frontend.Application.Write.Commands;

namespace Trsys.Frontend.Infrastructure
{
    public class CopyTradingEventHandler : BackgroundService
    {
        private readonly ILogger<CopyTradingEventHandler> logger;
        private readonly ICopyTradingEventBus eventBus;
        private readonly EaEventFlowRootResolver resolver;

        public CopyTradingEventHandler(ILogger<CopyTradingEventHandler> logger, ICopyTradingEventBus eventBus, EaEventFlowRootResolver resolver)
        {
            this.logger = logger;
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
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }
        }

        private async void OnCopyTradeEvent(ICopyTradingEvent copyTradingEvent)
        {
            try
            {
                switch (copyTradingEvent)
                {
                    case CopyTradingTradeOpenedEvent opened:
                        await HandleAsync(opened, CancellationToken.None);
                        break;
                    case CopyTradingTradeClosedEvent closed:
                        await HandleAsync(closed, CancellationToken.None);
                        break;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        }

        public async Task HandleAsync(CopyTradingTradeOpenedEvent domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            await Task.WhenAll(domainEvent.Subscribers.Select(subscriberId => Task.Run(async () =>
            {
                await commandBus.PublishAsync(new SubscriberOpenTradeOrderCommand(SubscriberId.With(subscriberId), CopyTradeId.With(domainEvent.CopyTradeId), new ForexTradeSymbol(domainEvent.Symbol), OrderType.Of(domainEvent.OrderType)), cancellationToken);
            })));
        }

        public async Task HandleAsync(CopyTradingTradeClosedEvent domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            await Task.WhenAll(domainEvent.Subscribers.Select(subscriberId => Task.Run(async () =>
            {
                await commandBus.PublishAsync(new SubscriberCloseTradeOrderCommand(SubscriberId.With(subscriberId), CopyTradeId.With(domainEvent.CopyTradeId)), cancellationToken);
            })));
        }
    }
}
