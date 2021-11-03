using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberOpenTradeOrderCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberOpenTradeOrderCommand(SubscriberId aggregateId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class SubscriberOpenTradeOrderCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberOpenTradeOrderCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberOpenTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
