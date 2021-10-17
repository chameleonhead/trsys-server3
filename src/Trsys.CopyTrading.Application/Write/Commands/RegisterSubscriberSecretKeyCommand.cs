using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class RegisterSubscriberSecretKeyCommand : Command<SecretKeyAggregate, SecretKeyId>
    {
        public RegisterSubscriberSecretKeyCommand(SecretKeyId aggregateId, SecretKey key, AccountId accountId) : base(aggregateId)
        {
            Key = key;
            AccountId = accountId;
        }

        public SecretKey Key { get; }
        public AccountId AccountId { get; }
    }

    public class RegisterSubscriberSecretKeyCommandHandler : CommandHandler<SecretKeyAggregate, SecretKeyId, RegisterSubscriberSecretKeyCommand>
    {
        public override Task ExecuteAsync(SecretKeyAggregate aggregate, RegisterSubscriberSecretKeyCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.AccountId);
            return Task.CompletedTask;
        }
    }
}
