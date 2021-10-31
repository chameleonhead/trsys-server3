using EventFlow;
using EventFlow.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Read.Queries;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.Core;

namespace Trsys.BackOffice.Infrastructure
{
    public class CopyTradeService : ICopyTradeService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public CopyTradeService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<PagedResult<CopyTradeDto>> SearchAsync(bool openOnly, int page, int perPage, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var totalCount = await queryProcessor.ProcessAsync(new CopyTradeReadModelSearchCountQuery(), cancellationToken);
            if (totalCount == 0)
            {
                return new PagedResult<CopyTradeDto>(page, perPage, 0, new());
            }
            var items = await queryProcessor.ProcessAsync(new CopyTradeReadModelSearchItemsQuery(page, perPage), cancellationToken);
            return new PagedResult<CopyTradeDto>(page, perPage, totalCount, items.Select(item => new CopyTradeDto()
            {
                Id = item.Id,
                DistributionGroupId = item.DistributionGroupId,
                Symbol = item.Symbol,
                OrderType = item.OrderType,
                IsClosed = item.IsClosed,
            }).ToList());
        }

        public async Task<CopyTradeDto> FindByIdAsync(string copyTradeId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var item = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CopyTradeReadModel>(copyTradeId), cancellationToken);
            if (item == null)
            {
                return null;
            }
            return new CopyTradeDto()
            {
                Id = item.Id,
                DistributionGroupId = item.DistributionGroupId,
                Symbol = item.Symbol,
                OrderType = item.OrderType,
                IsClosed = item.IsClosed,
            };
        }

        public async Task<string> OpenAsync(string distributionGroupId, string symbol, string orderType, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var copyTradeId = CopyTradeId.New;
            await commandBus.PublishAsync(new CopyTradeOpenCommand(copyTradeId, DistributionGroupId.With(distributionGroupId), new ForexTradeSymbol(symbol), OrderType.Of(orderType)), cancellationToken);
            return copyTradeId.Value;
        }

        public async Task CloseAsync(string copyTradeId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new CopyTradeCloseCommand(CopyTradeId.With(copyTradeId)), cancellationToken);
        }
    }
}