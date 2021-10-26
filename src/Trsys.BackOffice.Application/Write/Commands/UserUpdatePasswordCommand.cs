using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserChangePasswordCommand : Command<UserAggregate, UserId>
    {
        public UserChangePasswordCommand(UserId aggregateId, HashedPassword password) : base(aggregateId)
        {
            Password = password;
        }

        public HashedPassword Password { get; }
    }

    public class UserChangePasswordCommandHandler : CommandHandler<UserAggregate, UserId, UserChangePasswordCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserChangePasswordCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetPasswordHash(command.Password);
            return Task.CompletedTask;
        }
    }

}