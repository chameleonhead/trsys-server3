using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class SubscriberUpdateNameCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberUpdateNameCommand(SubscriberId aggregateId, SubscriberName name) : base(aggregateId)
        {
            Name = name;
        }

        public SubscriberName Name { get; }
    }

    public class SubscriberUpdateNameCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberUpdateNameCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberUpdateNameCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.CompletedTask;
        }
    }
}
