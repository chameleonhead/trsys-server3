using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Read.Queries;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice
{
    public class DistributionGroupService : IDistributionGroupService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public DistributionGroupService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<PagedResult<DistributionGroupDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var totalCount = await queryProcessor.ProcessAsync(new DistributionGroupReadModelSearchCountQuery(), cancellationToken);
            if (totalCount == 0)
            {
                return new PagedResult<DistributionGroupDto>(page, perPage, 0, new());
            }
            var items = await queryProcessor.ProcessAsync(new DistributionGroupReadModelSearchItemsQuery(page, perPage), cancellationToken);
            return new PagedResult<DistributionGroupDto>(page, perPage, totalCount, items.Select(e => new DistributionGroupDto()
            {
                Id = e.Id,
                DisplayName = e.DisplayName,
            }).ToList());
        }

        public async Task<DistributionGroupDto> FindByIdAsync(string distributionGroupId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var distributionGroup = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(distributionGroupId), cancellationToken);
            if (distributionGroup == null)
            {
                return null;
            }
            return new DistributionGroupDto()
            {
                Id = distributionGroup.Id,
                DisplayName = distributionGroup.DisplayName
            };
        }

        public async Task<string> CreateAsync(string displayName, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var distributionGroupId = DistributionGroupId.New;
            await commandBus.PublishAsync(new DistributionGroupCreateCommand(distributionGroupId, new DistributionGroupDisplayName(displayName)), cancellationToken);
            return distributionGroupId.Value;
        }

        public async Task DeleteAsync(string distributionGroupId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new DistributionGroupDeleteCommand(DistributionGroupId.With(distributionGroupId)), cancellationToken);
        }
    }
}