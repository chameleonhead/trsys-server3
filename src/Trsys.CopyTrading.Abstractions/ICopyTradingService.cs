using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading.Abstractions
{
    public interface ICopyTradingService
    {
        Task<DistributionGroupDto> FindDistributionGroupByIdAsync(string distributionGroupId, CancellationToken cancellationToken);
        Task<CopyTradeDto> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken);
        Task AddSubscriberAsync(string distributionGroupId, string subscriberId, CancellationToken cancellationToken);
        Task RemoveSubscriberAsync(string distributionGroupId, string subscriptionId, CancellationToken cancellationToken);
        Task PublishOpenTradeAsync(string distributionGroupId, string copyTradeId, string symbol, string orderType, CancellationToken cancellationToken);
        Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken);
    }
}
