using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IPublisherService
    {
        Task<PagedResult<PublisherDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken);
        Task<PublisherDto> FindByIdAsync(string publisherId, CancellationToken cancellationToken);
        Task<string> CreateAsync(string name, string description, CancellationToken cancellationToken);
        Task DeleteAsync(string publisherId, CancellationToken cancellationToken);
    }
}