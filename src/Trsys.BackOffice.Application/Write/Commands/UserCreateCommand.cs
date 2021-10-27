using EventFlow.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserCreateCommand : Command<UserAggregate, UserId>
    {
        public UserCreateCommand(UserId aggregateId, Username username, HashedPassword passwordHash, UserNickname nickname, IEnumerable<Role> roles) : base(aggregateId)
        {
            Username = username;
            PasswordHash = passwordHash;
            Nickname = nickname;
            Roles = roles;
        }

        public Username Username { get; }
        public HashedPassword PasswordHash { get; }
        public UserNickname Nickname { get; }
        public IEnumerable<Role> Roles { get; }
    }

    public class UserCreateCommandHandler : CommandHandler<UserAggregate, UserId, UserCreateCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetUsername(command.Username);
            aggregate.SetPasswordHash(command.PasswordHash);
            aggregate.SetNickname(command.Nickname);
            foreach (var role in command.Roles)
            {
                aggregate.AddRole(role);
            }
            return Task.CompletedTask;
        }
    }
}