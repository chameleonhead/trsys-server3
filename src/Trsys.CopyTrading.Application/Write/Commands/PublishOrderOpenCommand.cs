using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublishOrderOpenCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublishOrderOpenCommand(DistributionGroupId aggregateId, PublisherId publisherId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            PublisherId = publisherId;
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public PublisherId PublisherId { get; }
        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class PublishOrderOpenCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, PublishOrderOpenCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, PublishOrderOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.StartOpenDistribution(command.PublisherId, command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
