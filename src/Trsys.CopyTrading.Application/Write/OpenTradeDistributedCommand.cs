using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write
{
    public class OpenTradeDistributedCommand : Command<AccountAggregate, AccountId>
    {
        public OpenTradeDistributedCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class OpenTradeDistributedCommandHandler : CommandHandler<AccountAggregate, AccountId, OpenTradeDistributedCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, OpenTradeDistributedCommand command, CancellationToken cancellationToken)
        {
            aggregate.OpenTradeDistributed(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}