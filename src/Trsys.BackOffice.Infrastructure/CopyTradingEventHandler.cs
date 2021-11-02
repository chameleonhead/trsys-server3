using EventFlow;
using EventFlow.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;
using Trsys.CopyTrading.Abstractions;
using Trsys.Core;

namespace Trsys.BackOffice.Infrastructure
{
    public class CopyTradingEventHandler : BackgroundService
    {
        private readonly ILogger<CopyTradingEventHandler> logger;
        private readonly ICopyTradingEventBus eventBus;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public CopyTradingEventHandler(ILogger<CopyTradingEventHandler> logger, ICopyTradingEventBus eventBus, BackOfficeEventFlowRootResolver resolver)
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
                    case CopyTradingSubscriberAddedEvent subAdded:
                        await HandleAsync(subAdded, CancellationToken.None);
                        break;
                    case CopyTradingSubscriberRemovedEvent subRemoved:
                        await HandleAsync(subRemoved, CancellationToken.None);
                        break;
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

        private async Task HandleAsync(CopyTradingSubscriberAddedEvent domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            var distributionGroupId = DistributionGroupId.With(domainEvent.DistributionGroupId);
            var subscriberId = SubscriberId.With(domainEvent.SubscriberId);

            var distributionGroup = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(distributionGroupId.Value), cancellationToken);
            if (distributionGroup == null)
            {
                await commandBus.PublishAsync(new DistributionGroupCreateCommand(distributionGroupId, new DistributionGroupName(distributionGroupId.Value)), cancellationToken);
            }

            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(subscriberId.Value), cancellationToken);
            if (subscriber == null)
            {
                await commandBus.PublishAsync(new SubscriberCreateCommand(subscriberId, new SubscriberName(subscriberId.Value), null), cancellationToken);
            }
            await commandBus.PublishAsync(new DistributionGroupAddSubscriberCommand(distributionGroupId, subscriberId), cancellationToken);
        }

        private async Task HandleAsync(CopyTradingSubscriberRemovedEvent domainEvent, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var distributionGroupId = DistributionGroupId.With(domainEvent.DistributionGroupId);
            var subscriberId = SubscriberId.With(domainEvent.SubscriberId);
            await commandBus.PublishAsync(new DistributionGroupRemoveSubscriberCommand(distributionGroupId, subscriberId), cancellationToken);
        }

        private async Task HandleAsync(CopyTradingTradeOpenedEvent domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            var distributionGroupId = DistributionGroupId.With(domainEvent.DistributionGroupId);
            var copyTradeId = CopyTradeId.With(domainEvent.CopyTradeId);

            var distributionGroup = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(distributionGroupId.Value), cancellationToken);
            if (distributionGroup == null)
            {
                await commandBus.PublishAsync(new DistributionGroupCreateCommand(distributionGroupId, new DistributionGroupName(distributionGroupId.Value)), cancellationToken);
            }

            await commandBus.PublishAsync(new CopyTradeOpenCommand(copyTradeId, distributionGroupId, new ForexTradeSymbol(domainEvent.Symbol), OrderType.Of(domainEvent.OrderType)), cancellationToken);
        }

        private async Task HandleAsync(CopyTradingTradeClosedEvent domainEvent, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            var copyTradeId = CopyTradeId.With(domainEvent.CopyTradeId);

            var copyTrade = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CopyTradeReadModel>(copyTradeId.Value), cancellationToken);
            if (copyTrade == null)
            {
                throw new InvalidOperationException();
            }

            await commandBus.PublishAsync(new CopyTradeCloseCommand(copyTradeId), cancellationToken);
        }
    }
}
