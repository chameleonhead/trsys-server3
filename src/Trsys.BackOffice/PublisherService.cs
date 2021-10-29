using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public class PublisherService : IPublisherService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public PublisherService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public Task<PagedResult<PublisherDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<PublisherDto> FindByIdAsync(object publisherId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> CreateAsync(string name, string description, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(object publisherId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}