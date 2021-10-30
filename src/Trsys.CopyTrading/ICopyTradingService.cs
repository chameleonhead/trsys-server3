using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading
{
    public interface ICopyTradingService
    {
        Task<DistributionGroupDto> FindDistributionGroupByIdAsync(string distributionGroupId, CancellationToken cancellationToken);
        Task<CopyTradeDto> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken);
        Task<string> AddSubscriberAsync(string distributionGroupId, CancellationToken cancellationToken);
        Task RemoveSubscriberAsync(string distributionGroupId, string subscriptionId, CancellationToken cancellationToken);
        Task<string> PublishOpenTradeAsync(string distributionGroupId, string symbol, string orderType, CancellationToken cancellationToken);
        Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken);
    }
}
