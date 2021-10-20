using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeRemoveDistributedAccountCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeRemoveDistributedAccountCommand(CopyTradeId aggregateId, AccountId accountId) : base(aggregateId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }

    public class CopyTradeRemoveDistributedAccountCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeRemoveDistributedAccountCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeRemoveDistributedAccountCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemoveApplicant(command.AccountId);
            return Task.CompletedTask;
        }
    }
}
