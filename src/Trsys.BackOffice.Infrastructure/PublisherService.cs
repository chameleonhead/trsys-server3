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
    public class PublisherService : IPublisherService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public PublisherService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<PagedResult<PublisherDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var totalCount = await queryProcessor.ProcessAsync(new PublisherReadModelSearchCountQuery(), cancellationToken);
            if (totalCount == 0)
            {
                return new PagedResult<PublisherDto>(page, perPage, 0, new());
            }
            var items = await queryProcessor.ProcessAsync(new PublisherReadModelSearchItemsQuery(page, perPage), cancellationToken);
            return new PagedResult<PublisherDto>(page, perPage, totalCount, items.Select(item => new PublisherDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            }).ToList());
        }

        public async Task<PublisherDto> FindByIdAsync(string publisherId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var item = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(publisherId), cancellationToken);
            if (item == null)
            {
                return null;
            }
            return new PublisherDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }

        public async Task<string> CreateAsync(string name, string description, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var publisherId = PublisherId.New;
            await commandBus.PublishAsync(new PublisherCreateCommand(publisherId, new PublisherName(name), new PublisherDescription(description)), cancellationToken);
            return publisherId.Value;
        }

        public async Task UpdateNameAsync(string publisherId, string name, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new PublisherUpdateNameCommand(PublisherId.With(publisherId), new PublisherName(name)), cancellationToken);
        }

        public async Task UpdateDescriptionAsync(string publisherId, string description, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new PublisherUpdateDescriptionCommand(PublisherId.With(publisherId), new PublisherDescription(description)), cancellationToken);
        }

        public async Task DeleteAsync(string publisherId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new PublisherDeleteCommand(PublisherId.With(publisherId)), cancellationToken);
        }
    }
}