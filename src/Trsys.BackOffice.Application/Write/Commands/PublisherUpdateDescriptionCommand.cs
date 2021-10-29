using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class PublisherUpdateDescriptionCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherUpdateDescriptionCommand(PublisherId aggregateId, PublisherDescription description) : base(aggregateId)
        {
            Description = description;
        }

        public PublisherDescription Description { get; }
    }

    public class PublisherUpdateDescriptionCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherUpdateDescriptionCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherUpdateDescriptionCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetDescription(command.Description);
            return Task.CompletedTask;
        }
    }
}
