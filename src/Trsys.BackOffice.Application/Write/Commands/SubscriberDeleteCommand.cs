using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class SubscriberDeleteCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberDeleteCommand(SubscriberId aggregateId) : base(aggregateId)
        {
        }
    }

    public class SubscriberDeleteCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberDeleteCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberDeleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.Delete();
            return Task.CompletedTask;
        }
    }
}