using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeAddDistributedAccountCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeAddDistributedAccountCommand(CopyTradeId aggregateId, AccountId accountId) : base(aggregateId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }

    public class CopyTradeAddDistributedAccountCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeAddDistributedAccountCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeAddDistributedAccountCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddApplicant(command.AccountId);
            return Task.CompletedTask;
        }
    }
}