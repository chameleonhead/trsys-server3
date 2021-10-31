using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class CopyTradeOpenCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenCommand(CopyTradeId aggregateId, DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class CopyTradeOpenCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeOpenCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.DistributionGroupId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}