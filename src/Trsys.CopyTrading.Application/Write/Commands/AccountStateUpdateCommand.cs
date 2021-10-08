using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountStateUpdateCommand : Command<AccountAggregate, AccountId>
    {
        public AccountStateUpdateCommand(AccountId aggregateId, AccountBalance balance) : base(aggregateId)
        {
            Balance = balance;
        }

        public AccountBalance Balance { get; }
    }
    public class AccountStateUpdateCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountStateUpdateCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountStateUpdateCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateState(command.Balance);
            return Task.CompletedTask;
        }
    }
}
