using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeCloseCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeCloseCommand(CopyTradeId aggregateId, DistributionGroupId aggregateIdentity, PublisherId publisherId) : base(aggregateId)
        {
            AggregateIdentity = aggregateIdentity;
            PublisherId = publisherId;
        }

        public DistributionGroupId AggregateIdentity { get; }
        public PublisherId PublisherId { get; }
    }

    public class CopyTradeCloseCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeCloseCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.PublisherId);
            return Task.CompletedTask;
        }
    }
}