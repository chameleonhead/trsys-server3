using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Application.Write.Subscribers;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading
{
    public class CopyTradingService : ICopyTradingService
    {
        private readonly CopyTradingEventFlowRootResolver resolver;
        private readonly AllEventBus eventBus;

        public CopyTradingService(CopyTradingEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
            eventBus = resolver.Resolve<AllEventBus>();
        }

        public async Task<DistributionGroupDto> FindDistributionGroupByIdAsync(string distributionGroupId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var distirbutionGroup = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(distributionGroupId), cancellationToken);
            if (distirbutionGroup == null)
            {
                return null;
            }
            return new DistributionGroupDto()
            {
                Id = distirbutionGroup.Id,
                Subscribers = distirbutionGroup.Subscribers.ToList(),
            };
        }

        public async Task<CopyTradeDto> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var copyTrade = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CopyTradeReadModel>(copyTradeId), cancellationToken);
            if (copyTrade == null)
            {
                return null;
            }
            return new CopyTradeDto()
            {
                Id = copyTrade.Id,
                DistributionGroupId = copyTrade.DistributionGroupId,
                Symbol = copyTrade.Symbol,
                OrderType = copyTrade.OrderType,
                Subscribers = copyTrade.Subscribers,
                IsOpen = copyTrade.IsOpen,
            };
        }

        public async Task AddSubscriberAsync(string distributionGroupId, string subscriptionId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupAddSubscriberCommand(DistributionGroupId.With(distributionGroupId), SubscriberId.With(subscriptionId)), cancellationToken);
        }

        public async Task RemoveSubscriberAsync(string distributionGroupId, string subscriptionId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupRemoveSubscriberCommand(DistributionGroupId.With(distributionGroupId), SubscriberId.With(subscriptionId)), cancellationToken);
        }

        public async Task PublishOpenTradeAsync(string distributionGroupId, string copyTradeId, string symbol, string orderType, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupPublishOpenCommand(DistributionGroupId.With(distributionGroupId), CopyTradeId.With(copyTradeId), new ForexTradeSymbol(symbol), OrderType.Of(orderType)), cancellationToken);
        }

        public async Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupPublishCloseCommand(DistributionGroupId.With(distributionGroupId), CopyTradeId.With(copyTradeId)), cancellationToken);
        }

        public Task SubscribeToCopyTradeEventsAsync(Func<ICopyTradingEvent, Task> onCopyTradeEvent, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                foreach (var events in eventBus.Events.GetConsumingEnumerable(cancellationToken))
                {
                    foreach (var e in events)
                    {
                        if (e is IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> opened)
                        {
                            onCopyTradeEvent.Invoke(new CopyTradeOpened(opened.AggregateIdentity.Value, opened.AggregateEvent.DistributionGroupId.Value, opened.AggregateEvent.Symbol.Value, opened.AggregateEvent.OrderType.Value, opened.AggregateEvent.Subscribers.Select(subscriberId => subscriberId.Value).ToList()));
                        }
                        if (e is IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> closed)
                        {
                            onCopyTradeEvent.Invoke(new CopyTradeClosed(closed.AggregateIdentity.Value, closed.AggregateEvent.Subscribers.Select(subscriberId => subscriberId.Value).ToList()));
                        }
                    }
                }
            });
        }
    }
}
