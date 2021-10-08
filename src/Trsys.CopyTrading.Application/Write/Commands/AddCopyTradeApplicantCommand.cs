using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AddCopyTradeApplicantCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public AddCopyTradeApplicantCommand(CopyTradeId aggregateId, AccountId accountId) : base(aggregateId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }

    public class AddCopyTradeApplicantCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, AddCopyTradeApplicantCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, AddCopyTradeApplicantCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddApplicant(command.AccountId);
            return Task.CompletedTask;
        }
    }
}