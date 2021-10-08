using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CloseTradeCommand : Command<AccountAggregate, AccountId>
    {
        public CloseTradeCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class CloseTradeCommandHandler : CommandHandler<AccountAggregate, AccountId, CloseTradeCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, CloseTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.CloseTrade(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}