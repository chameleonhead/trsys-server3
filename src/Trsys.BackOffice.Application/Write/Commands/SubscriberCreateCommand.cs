using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class SubscriberCreateCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberCreateCommand(SubscriberId aggregateId, SubscriberName name, SubscriberDescription description) : base(aggregateId)
        {
            Name = name;
            Description = description;
        }

        public SubscriberName Name { get; }
        public SubscriberDescription Description { get; }
    }

    public class SubscriberCreateCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberCreateCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            aggregate.SetDescription(command.Description);
            return Task.CompletedTask;
        }
    }
}