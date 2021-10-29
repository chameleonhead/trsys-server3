using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface ISubscriberService
    {
        Task<PagedResult<SubscriberDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken);
        Task<SubscriberDto> FindByIdAsync(object subscriberId, CancellationToken cancellationToken);
        Task<string> CreateAsync(string name, string description, CancellationToken cancellationToken);
        Task DeleteAsync(object subscriberId, CancellationToken cancellationToken);
    }
}