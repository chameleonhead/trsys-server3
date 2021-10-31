using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class CopyTradeCloseCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeCloseCommand(CopyTradeId aggregateId) : base(aggregateId)
        {
        }
    }

    public class CopyTradeCloseCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeCloseCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close();
            return Task.CompletedTask;
        }
    }
}