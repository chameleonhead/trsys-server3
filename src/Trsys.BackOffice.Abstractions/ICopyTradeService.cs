using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice.Abstractions
{
    public interface ICopyTradeService
    {
        Task<PagedResult<CopyTradeDto>> SearchAsync(bool openOnly, int page, int perPage, CancellationToken cancellationToken);
        Task<CopyTradeDto> FindByIdAsync(string copyTradeId, CancellationToken cancellationToken);
        Task<string> OpenAsync(string distributionGroupId, string symbol, string orderType, CancellationToken cancellationToken);
        Task CloseAsync(string copyTradeId, CancellationToken cancellationToken);
    }
}