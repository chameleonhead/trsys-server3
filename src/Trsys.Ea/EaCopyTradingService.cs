using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading;

namespace Trsys.Ea
{
    public class EaCopyTradingService : Application.ICopyTradingService
    {
        private readonly ICopyTradingService service;

        public EaCopyTradingService(ICopyTradingService service)
        {
            this.service = service;
        }

        public Task AddSubscriberAsync(string distributionGroupId, string subscriberId, CancellationToken cancellationToken)
        {
            return service.AddSubscriberAsync(distributionGroupId, subscriberId, cancellationToken);
        }

        public Task RemoveSubscriberAsync(string distributionGroupId, string subscriberId, CancellationToken cancellationToken)
        {
            return service.RemoveSubscriberAsync(distributionGroupId, subscriberId, cancellationToken);
        }

        public Task PublishOpenTradeAsync(string distributionGroupId, string copyTradeId, string symbol, string orderType, CancellationToken cancellationToken)
        {
            return service.PublishOpenTradeAsync(distributionGroupId, copyTradeId, symbol, orderType, cancellationToken);
        }

        public Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken)
        {
            return service.PublishCloseTradeAsync(distributionGroupId, copyTradeId, cancellationToken);
        }
    }
}
