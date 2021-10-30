using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupPublishOpenCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublishOpenCommand(DistributionGroupId aggregateId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class DistributionGroupPublishOpenCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupPublishOpenCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupPublishOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.PublishOpen(command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
