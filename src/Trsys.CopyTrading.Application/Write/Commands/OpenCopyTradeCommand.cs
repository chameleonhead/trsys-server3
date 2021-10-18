using EventFlow.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class OpenCopyTradeCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public OpenCopyTradeCommand(CopyTradeId aggregateId, DistributionGroupId distributionGroupId, PublisherId publisherId, int sequence, ForexTradeSymbol symbol, OrderType orderType, List<AccountId> subscribers) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
            Sequence = sequence;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
        public int Sequence { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<AccountId> Subscribers { get; }
    }

    public class OpenCopyTradeCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, OpenCopyTradeCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, OpenCopyTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.DistributionGroupId, command.PublisherId, command.Sequence, command.Symbol, command.OrderType, command.Subscribers);
            return Task.CompletedTask;
        }
    }
}
