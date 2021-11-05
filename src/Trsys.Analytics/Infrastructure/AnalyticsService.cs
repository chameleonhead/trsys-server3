using EventFlow;
using EventFlow.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Analytics.Abstractions;
using Trsys.Analytics.Application.Read.Models;
using Trsys.Analytics.Application.Write.Commands;
using Trsys.Analytics.Domain;
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
                PublisherTradeResult = new PublisherTradeResultDto() {
                    Id = copyTrade.PublisherId,
                    Duration = new TradeDurationDto() {
                        OpenedAt = copyTrade.PublisherOpenedAt,
                        ClosedAt = copyTrade.PublisherClosedAt,
                    },
                    Score = new TradeScoreDto() {
                        OrderType = copyTrade.PublisherOrderType,
                        PriceOpened = copyTrade.PublisherPriceOpened,
                        PriceClosed = copyTrade.PublisherPriceClosed,
                    }
                }
            };
        }

        public async Task OpenCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new CopyTradeOpenCommand(CopyTradeId.With(copyTradeId), timestamp, ForexTradeSymbol.ValueOf(symbol), OrderType.ValueOf(orderType)), cancellationToken);
        }

        public async Task CloseCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new CopyTradeCloseCommand(CopyTradeId.With(copyTradeId), timestamp), cancellationToken);
        }

        public async Task PublisherOpenCopyTradeAsync(string publisherId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var tradeSymbol = ForexTradeSymbol.ValueOf(symbol);
            await commandBus.PublishAsync(new PublisherOpenCopyTradeCommand(PublisherId.With(publisherId), CopyTradeId.With(copyTradeId), timestamp, tradeSymbol, OrderType.ValueOf(orderType), tradeSymbol.Quote.PriceOf(price), new Lot(lots)), cancellationToken);
        }

        public Task PublisherCloseCopyTradeAsync(string publisherId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, decimal profit, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SubscriberOpenCopyTradeAsync(string subscriberId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SubscriberCloseCopyTradeAsync(string subscriberId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, decimal profit, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}