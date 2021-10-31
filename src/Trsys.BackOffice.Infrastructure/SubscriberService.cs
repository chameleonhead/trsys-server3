using EventFlow;
using EventFlow.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Read.Queries;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Infrastructure
{
    public class SubscriberService : ISubscriberService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public SubscriberService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<PagedResult<SubscriberDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var totalCount = await queryProcessor.ProcessAsync(new SubscriberReadModelSearchCountQuery(), cancellationToken);
            if (totalCount == 0)
            {
                return new PagedResult<SubscriberDto>(page, perPage, 0, new());
            }
            var items = await queryProcessor.ProcessAsync(new SubscriberReadModelSearchItemsQuery(page, perPage), cancellationToken);
            return new PagedResult<SubscriberDto>(page, perPage, totalCount, items.Select(item => new SubscriberDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            }).ToList());
        }

        public async Task<SubscriberDto> FindByIdAsync(string subscriberId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var item = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(subscriberId), cancellationToken);
            if (item == null)
            {
                return null;
            }
            return new SubscriberDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }

        public async Task<string> CreateAsync(string name, string description, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var subscriberId = SubscriberId.New;
            await commandBus.PublishAsync(new SubscriberCreateCommand(subscriberId, new SubscriberName(name), new SubscriberDescription(description)), cancellationToken);
            return subscriberId.Value;
        }

        public async Task UpdateNameAsync(string subscriberId, string name, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new SubscriberUpdateNameCommand(SubscriberId.With(subscriberId), new SubscriberName(name)), cancellationToken);
        }

        public async Task UpdateDescriptionAsync(string subscriberId, string description, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new SubscriberUpdateDescriptionCommand(SubscriberId.With(subscriberId), new SubscriberDescription(description)), cancellationToken);
        }

        public async Task DeleteAsync(string subscriberId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new SubscriberDeleteCommand(SubscriberId.With(subscriberId)), cancellationToken);
        }
    }
}