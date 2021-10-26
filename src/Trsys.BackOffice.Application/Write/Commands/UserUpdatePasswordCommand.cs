using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserUpdatePasswordCommand : Command<UserAggregate, UserId>
    {
        public UserUpdatePasswordCommand(UserId aggregateId, HashedPassword password) : base(aggregateId)
        {
            Password = password;
        }

        public HashedPassword Password { get; }
    }

    public class UserUpdatePasswordCommandHandler : CommandHandler<UserAggregate, UserId, UserUpdatePasswordCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserUpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetPasswordHash(command.Password);
            return Task.CompletedTask;
        }
    }

}