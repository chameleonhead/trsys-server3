using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupPublishOpenCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublishOpenCommand(DistributionGroupId aggregateId, PublisherId publisherId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
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

    public class DistributionGroupPublishOpenCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupPublishOpenCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupPublishOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.PublishOpen(command.PublisherId, command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
