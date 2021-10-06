﻿using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write
{
    public class PublishOrderOpenCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public PublishOrderOpenCommand(CopyTradeId aggregateId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            Symbol = symbol;
            OrderType = orderType;
        }

        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class PublishOrderOpenCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, PublishOrderOpenCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, PublishOrderOpenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}
