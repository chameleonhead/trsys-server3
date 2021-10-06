using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write
{
    public class StartTradeDistributionCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public StartTradeDistributionCommand(DistributionGroupId aggregateId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class StartTradeDistributionCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, StartTradeDistributionCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, StartTradeDistributionCommand command, CancellationToken cancellationToken)
        {
            aggregate.StartDistribution(command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
