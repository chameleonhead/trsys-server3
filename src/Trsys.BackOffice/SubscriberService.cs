using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public class SubscriberService : ISubscriberService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public SubscriberService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public Task<PagedResult<SubscriberDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<SubscriberDto> FindByIdAsync(object subscriberId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> CreateAsync(string name, string description, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(object subscriberId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}