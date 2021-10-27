using EventFlow.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserUpdateRolesCommand : Command<UserAggregate, UserId>
    {
        public UserUpdateRolesCommand(UserId aggregateId, IEnumerable<Role> roles) : base(aggregateId)
        {
            Roles = roles;
        }

        public IEnumerable<Role> Roles { get; }
    }

    public class UserUpdateRolesCommandHandler : CommandHandler<UserAggregate, UserId, UserUpdateRolesCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserUpdateRolesCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetRoles(command.Roles);
            return Task.CompletedTask;
        }
    }

}