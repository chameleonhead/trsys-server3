using EventFlow;
using EventFlow.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading
{
    public class CopyTradingService : ICopyTradingService
    {
        private readonly CopyTradingEventFlowRootResolver resolver;

        public CopyTradingService(CopyTradingEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
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

        public async Task<string> AddSubscriberAsync(string distributionGroupId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var subscriptionId = SubscriberId.New;
            await commandBus.PublishAsync(new DistributionGroupAddSubscriberCommand(DistributionGroupId.With(distributionGroupId), subscriptionId), cancellationToken);
            return subscriptionId.Value;
        }

        public async Task RemoveSubscriberAsync(string distributionGroupId, string subscriptionId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupRemoveSubscriberCommand(DistributionGroupId.With(distributionGroupId), SubscriberId.With(subscriptionId)), cancellationToken);
        }

        public async Task<string> PublishOpenTradeAsync(string distributionGroupId, string symbol, string orderType, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var copyTradeId = CopyTradeId.New;
            await commandBus.PublishAsync(new DistributionGroupPublishOpenCommand(DistributionGroupId.With(distributionGroupId), copyTradeId, new ForexTradeSymbol(symbol), OrderType.Of(orderType)), cancellationToken);
            return copyTradeId.Value;
        }

        public async Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupPublishCloseCommand(DistributionGroupId.With(distributionGroupId), CopyTradeId.With(copyTradeId)), cancellationToken);
        }
    }
}
