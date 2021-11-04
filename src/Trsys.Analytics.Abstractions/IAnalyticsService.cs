using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Analytics.Abstractions
{
    public interface IAnalyticsService
    {
        Task<CopyTradeDto> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken);
        Task OpenCopyTradeAsync(string copyTradeId, DateTimeOffset dateTimeOffset, string symbol, string orderType);
    }
}
