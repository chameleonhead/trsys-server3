using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class SubscriberCreateCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberCreateCommand(SubscriberId aggregateId, SubscriberName name) : base(aggregateId)
        {
            Name = name;
        }

        public SubscriberName Name { get; }
    }

    public class SubscriberCreateCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberCreateCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.CompletedTask;
        }
    }
}