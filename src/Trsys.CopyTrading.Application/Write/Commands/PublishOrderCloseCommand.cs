using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublishOrderCloseCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublishOrderCloseCommand(DistributionGroupId aggregateId, PublisherId publisherId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            PublisherId = publisherId;
            CopyTradeId = copyTradeId;
        }

        public PublisherId PublisherId { get; }
        public CopyTradeId CopyTradeId { get; }
    }

    public class PublishOrderCloseCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, PublishOrderCloseCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, PublishOrderCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.StartCloseDistribution(command.PublisherId, command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}
