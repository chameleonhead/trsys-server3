using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AddCopyTradeDistributedAccountCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public AddCopyTradeDistributedAccountCommand(CopyTradeId aggregateId, AccountId accountId) : base(aggregateId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }

    public class AddCopyTradeDistributedAccountCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, AddCopyTradeDistributedAccountCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, AddCopyTradeDistributedAccountCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddApplicant(command.AccountId);
            return Task.CompletedTask;
        }
    }
}