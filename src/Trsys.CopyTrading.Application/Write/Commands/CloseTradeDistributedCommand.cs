using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CloseTradeDistributedCommand : Command<AccountAggregate, AccountId>
    {
        public CloseTradeDistributedCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class CloseTradeDistributedCommandHandler : CommandHandler<AccountAggregate, AccountId, CloseTradeDistributedCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, CloseTradeDistributedCommand command, CancellationToken cancellationToken)
        {
            aggregate.CloseTradeDistributed(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}