using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class SubscriberEaOpenTradeOrderCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaOpenTradeOrderCommand(SubscriberEaId aggregateId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class SubscriberEaOpenTradeOrderCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOpenTradeOrderCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaOpenTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
