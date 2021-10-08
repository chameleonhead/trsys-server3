using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class OpenTradeCommand : Command<AccountAggregate, AccountId>
    {
        public OpenTradeCommand(AccountId aggregateId, CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, TradeQuantity quantity) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Symbol = symbol;
            OrderType = orderType;
            Quantity = quantity;
        }

        public CopyTradeId CopyTradeId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public TradeQuantity Quantity { get; }
    }

    public class OpenTradeCommandHandler : CommandHandler<AccountAggregate, AccountId, OpenTradeCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, OpenTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.OpenTrade(command.CopyTradeId, command.Symbol, command.OrderType, command.Quantity);
            return Task.CompletedTask;
        }
    }
}