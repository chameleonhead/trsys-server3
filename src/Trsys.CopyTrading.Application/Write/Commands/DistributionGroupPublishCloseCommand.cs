using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupPublishCloseCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublishCloseCommand(DistributionGroupId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class DistributionGroupPublishCloseCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupPublishCloseCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupPublishCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.PublishClose(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}
