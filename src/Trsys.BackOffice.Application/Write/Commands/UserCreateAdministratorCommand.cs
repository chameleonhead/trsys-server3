using EventFlow.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserCreateAdministratorCommand : Command<UserAggregate, UserId>
    {
        public UserCreateAdministratorCommand(UserId aggregateId, Username username, HashedPassword password, UserNickname nickname, List<DistributionGroupId> inChargeDistributionGroups) : base(aggregateId)
        {
            Username = username;
            Password = password;
            Nickname = nickname;
            InChargeDistributionGroups = inChargeDistributionGroups;
        }

        public Username Username { get; }
        public HashedPassword Password { get; }
        public UserNickname Nickname { get; }
        public List<DistributionGroupId> InChargeDistributionGroups { get; }
    }

    public class UserCreateAdministratorCommandHandler : CommandHandler<UserAggregate, UserId, UserCreateAdministratorCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserCreateAdministratorCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.Username, command.Nickname);
            aggregate.AddRole(Role.Administrator);
            aggregate.SetPassword(command.Password);
            aggregate.SetInChargeDistributionGroup(command.InChargeDistributionGroups);
            return Task.CompletedTask;
        }
    }

}