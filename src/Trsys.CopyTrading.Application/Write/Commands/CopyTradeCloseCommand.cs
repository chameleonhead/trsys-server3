using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeCloseCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeCloseCommand(CopyTradeId aggregateId, DistributionGroupId aggregateIdentity) : base(aggregateId)
        {
            AggregateIdentity = aggregateIdentity;
        }

        public DistributionGroupId AggregateIdentity { get; }
    }

    public class CopyTradeCloseCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeCloseCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close();
            return Task.CompletedTask;
        }
    }
}