using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserCreateCommand : Command<UserAggregate, UserId>
    {
        public UserCreateCommand(UserId aggregateId, Username username, HashedPassword password) : base(aggregateId)
        {
            Username = username;
            Password = password;
        }

        public Username Username { get; }
        public HashedPassword Password { get; }
    }

    public class UserCreateCommandHandler : CommandHandler<UserAggregate, UserId, UserCreateCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.Username, command.Password);
            return Task.CompletedTask;
        }
    }

}