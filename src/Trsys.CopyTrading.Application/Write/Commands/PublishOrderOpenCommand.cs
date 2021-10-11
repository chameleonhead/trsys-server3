using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublishOrderOpenCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public PublishOrderOpenCommand(CopyTradeId aggregateId, PublisherIdentifier publisherId, DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            PublisherId = publisherId;
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public PublisherIdentifier PublisherId { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class PublishOrderOpenCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, PublishOrderOpenCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, PublishOrderOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.PublisherId, command.DistributionGroupId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
