using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class UserUpdateNicknameCommand : Command<UserAggregate, UserId>
    {
        public UserUpdateNicknameCommand(UserId aggregateId, UserNickname nickname) : base(aggregateId)
        {
            Nickname = nickname;
        }

        public UserNickname Nickname { get; }
    }

    public class UserUpdateNicknameCommandHandler : CommandHandler<UserAggregate, UserId, UserUpdateNicknameCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserUpdateNicknameCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetNickname(command.Nickname);
            return Task.CompletedTask;
        }
    }
}