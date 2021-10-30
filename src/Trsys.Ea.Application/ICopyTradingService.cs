using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Ea.Application
{
    public interface ICopyTradingService
    {
        Task AddSubscriberAsync(string distributionGroupId, string subscriberId, CancellationToken cancellationToken);
        Task RemoveSubscriberAsync(string distributionGroupId, string subscriberId, CancellationToken cancellationToken);
        Task PublishOpenTradeAsync(string distributionGroupId, string copyTradeId, string symbol, string orderType, CancellationToken cancellationToken);
        Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken);
    }
}
