using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublishOrderCloseCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublishOrderCloseCommand(DistributionGroupId aggregateId, CopyTradeId copyTradeId, ClientKey clientKey) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            ClientKey = clientKey;
        }

        public CopyTradeId CopyTradeId { get; }
        public ClientKey ClientKey { get; }
    }

    public class PublishOrderCloseCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, PublishOrderCloseCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, PublishOrderCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.StartCloseDistribution(command.CopyTradeId, command.ClientKey);
            return Task.CompletedTask;
        }
    }
}
