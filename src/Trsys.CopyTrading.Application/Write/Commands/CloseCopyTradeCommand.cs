using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CloseCopyTradeCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CloseCopyTradeCommand(CopyTradeId aggregateId, DistributionGroupId aggregateIdentity, PublisherId publisherId) : base(aggregateId)
        {
            AggregateIdentity = aggregateIdentity;
            PublisherId = publisherId;
        }

        public DistributionGroupId AggregateIdentity { get; }
        public PublisherId PublisherId { get; }
    }

    public class CloseCopyTradeCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CloseCopyTradeCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CloseCopyTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.PublisherId);
            return Task.CompletedTask;
        }
    }
}