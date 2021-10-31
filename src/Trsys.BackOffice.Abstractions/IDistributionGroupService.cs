using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice.Abstractions
{
    public interface IDistributionGroupService
    {
        Task<PagedResult<DistributionGroupDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken);
        Task<DistributionGroupDto> FindByIdAsync(string distributionGroupId, CancellationToken cancellationToken);
        Task<string> CreateAsync(string name, CancellationToken cancellationToken);
        Task UpdateNameAsync(string distributionGroupId, string name, CancellationToken cancellationToken);
        Task DeleteAsync(string distributionGroupId, CancellationToken cancellationToken);
    }
}