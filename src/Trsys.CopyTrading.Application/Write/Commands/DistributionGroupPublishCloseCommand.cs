using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupPublishCloseCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublishCloseCommand(DistributionGroupId aggregateId, PublisherId publisherId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            PublisherId = publisherId;
            CopyTradeId = copyTradeId;
        }

        public PublisherId PublisherId { get; }
        public CopyTradeId CopyTradeId { get; }
    }

    public class DistributionGroupPublishCloseCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupPublishCloseCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupPublishCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.PublishClose(command.PublisherId, command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}
