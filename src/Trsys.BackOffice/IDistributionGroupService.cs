using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IDistributionGroupService
    {
        Task<PagedResult<DistributionGroupDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken);
        Task<DistributionGroupDto> FindByIdAsync(string distributionGroupId, CancellationToken cancellationToken);
        Task<string> CreateAsync(string displayName, CancellationToken cancellationToken);
        Task DeleteAsync(string distributionGroupId, CancellationToken cancellationToken);
    }
}