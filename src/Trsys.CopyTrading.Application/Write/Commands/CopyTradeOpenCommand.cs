using EventFlow.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeOpenCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenCommand(CopyTradeId aggregateId, DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType, List<SubscriberId> subscribers) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<SubscriberId> Subscribers { get; }
    }

    public class CopyTradeOpenCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeOpenCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.DistributionGroupId, command.Symbol, command.OrderType, command.Subscribers);
            return Task.CompletedTask;
        }
    }
}
