using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Analytics.Domain;
using Trsys.Core;

namespace Trsys.Analytics.Application.Write.Commands
{
    public class CopyTradeOpenCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenCommand(CopyTradeId aggregateId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            Timestamp = timestamp;
            Symbol = symbol;
            OrderType = orderType;
        }

        public DateTimeOffset Timestamp { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class CopyTradeOpenCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeOpenCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.Timestamp, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }

}
