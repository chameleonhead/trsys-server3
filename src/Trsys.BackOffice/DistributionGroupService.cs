using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Trsys.BackOffice.Application.Read.Models;
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