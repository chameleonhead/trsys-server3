using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Analytics.Abstractions
{
    public interface IAnalyticsService
    {
        Task<CopyTradeDto?> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken);
        Task OpenCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, CancellationToken cancellationToken);
        Task CloseCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, CancellationToken cancellationToken);
        Task PublisherOpenCopyTradeAsync(string publisherId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, CancellationToken cancellationToken);
        Task PublisherCloseCopyTradeAsync(string publisherId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, decimal profit, CancellationToken cancellationToken);
        Task SubscriberOpenCopyTradeAsync(string subscriberId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, CancellationToken cancellationToken);
        Task SubscriberCloseCopyTradeAsync(string subscriberId, string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, decimal price, decimal lots, decimal profit, CancellationToken cancellationToken);
    }
}
