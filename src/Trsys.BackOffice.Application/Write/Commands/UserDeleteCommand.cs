using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice
{
    public class UserDeleteCommand : Command<UserAggregate, UserId>
    {
        public UserDeleteCommand(UserId aggregateId) : base(aggregateId)
        {
        }
    }

    public class UserDeleteCommandHandler : CommandHandler<UserAggregate, UserId, UserDeleteCommand>
    {
        public override Task ExecuteAsync(UserAggregate aggregate, UserDeleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.Delete();
            return Task.CompletedTask;
        }
    }
}