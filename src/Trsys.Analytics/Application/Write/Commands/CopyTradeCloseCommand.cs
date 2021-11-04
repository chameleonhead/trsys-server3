using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Analytics.Domain;
using Trsys.Core;

namespace Trsys.Analytics.Application.Write.Commands
{
    public class CopyTradeCloseCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeCloseCommand(CopyTradeId aggregateId, DateTimeOffset timestamp) : base(aggregateId)
        {
            Timestamp = timestamp;
        }

        public DateTimeOffset Timestamp { get; }
    }

    public class CopyTradeCloseCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeCloseCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.Timestamp);
            return Task.CompletedTask;
        }
    }

}
