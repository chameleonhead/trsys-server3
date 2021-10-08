using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributeOpenTradeCommand : Command<AccountAggregate, AccountId>
    {
        public DistributeOpenTradeCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class DistributeOpenTradeCommandHandler : CommandHandler<AccountAggregate, AccountId, DistributeOpenTradeCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, DistributeOpenTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.OpenTradeDistributed(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}