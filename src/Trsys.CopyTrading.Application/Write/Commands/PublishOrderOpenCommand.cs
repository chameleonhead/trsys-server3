using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublishOrderOpenCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public PublishOrderOpenCommand(DistributionGroupId aggregateId, CopyTradeId copyTradeId, PublisherIdentifier clientKey, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            ClientKey = clientKey;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public PublisherIdentifier ClientKey { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class PublishOrderOpenCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, PublishOrderOpenCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, PublishOrderOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.StartOpenDistribution(command.CopyTradeId, command.ClientKey, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
