using EventFlow;
using EventFlow.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Analytics.Abstractions;
using Trsys.Analytics.Application.Read.Models;
using Trsys.Analytics.Application.Write.Commands;
using Trsys.Core;

namespace Trsys.Analytics.Infrastructure
{
    internal class AnalyticsService : IAnalyticsService
    {
        private readonly AnalyticsEventFlowRootResolver resolver;

        public AnalyticsService(AnalyticsEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<CopyTradeDto?> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken)
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
                Duration = new TradeDurationDto()
                {
                    OpenedAt = copyTrade.OpenedAt,
                    ClosedAt = copyTrade.ClosedAt,
                },
                Symbol = copyTrade.Symbol,
                OrderType = copyTrade.OrderType,
            };
        }

        public async Task OpenCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new CopyTradeOpenCommand(CopyTradeId.With(copyTradeId), timestamp, new ForexTradeSymbol(symbol), OrderType.Of(orderType)), cancellationToken);
        }

        public async Task CloseCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new CopyTradeCloseCommand(CopyTradeId.With(copyTradeId), timestamp), cancellationToken);
        }
    }
}