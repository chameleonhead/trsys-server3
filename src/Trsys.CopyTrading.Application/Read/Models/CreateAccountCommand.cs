using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CreateAccountCommand : Command<AccountAggregate, AccountId>
    {
        public CreateAccountCommand(AccountId aggregateId, ClientKey clientKey) : base(aggregateId)
        {
            ClientKey = clientKey;
        }

        public ClientKey ClientKey { get; }
    }

    public class CreateAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, CreateAccountCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, CreateAccountCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetClientKey(command.ClientKey);
            return Task.CompletedTask;
        }
    }
}
