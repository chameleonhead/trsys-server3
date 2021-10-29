using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class SubscriberUpdateDescriptionCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberUpdateDescriptionCommand(SubscriberId aggregateId, SubscriberDescription description) : base(aggregateId)
        {
            Description = description;
        }

        public SubscriberDescription Description { get; }
    }

    public class SubscriberUpdateDescriptionCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberUpdateDescriptionCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberUpdateDescriptionCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetDescription(command.Description);
            return Task.CompletedTask;
        }
    }
}
