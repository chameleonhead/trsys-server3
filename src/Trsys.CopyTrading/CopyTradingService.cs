using EventFlow;
using EventFlow.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading
{
    public class CopyTradingService : ICopyTradingService
    {
        private readonly CopyTradingEventFlowRootResolver resolver;

        public CopyTradingService(CopyTradingEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<DistributionGroupDto> FindDistributionGroupByIdAsync(string distributionGroupId, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var distirbutionGroup = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(distributionGroupId), cancellationToken);
            if (distirbutionGroup == null)
            {
                return null;
            }
            return new DistributionGroupDto()
            {
                Id = distirbutionGroup.Id,
                Subscribers = distirbutionGroup.Subscribers.ToList(),
            };
        }

        public async Task<string> AddSubscriberAsync(string distributionGroupId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            var subscriptionId = AccountId.New;
            await commandBus.PublishAsync(new DistributionGroupAddSubscriberCommand(DistributionGroupId.With(distributionGroupId), subscriptionId), cancellationToken);
            return subscriptionId.Value;
        }

        public Task PublishCloseTradeAsync(string distributionGroupId, string copyTradeId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PublishOpenTradeAsync(string distributionGroupId, string symbol, string orderType, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveSubscriberAsync(string distributionGroupId, string subscriptionId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
