using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributeCloseTradeCommand : Command<AccountAggregate, AccountId>
    {
        public DistributeCloseTradeCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class DistributeCloseTradeCommandHandler : CommandHandler<AccountAggregate, AccountId, DistributeCloseTradeCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, DistributeCloseTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.CloseTradeDistributed(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}