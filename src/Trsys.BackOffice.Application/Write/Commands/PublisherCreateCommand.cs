using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class PublisherCreateCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherCreateCommand(PublisherId aggregateId, PublisherName name) : base(aggregateId)
        {
            Name = name;
        }

        public PublisherName Name { get; }
    }

    public class PublisherCreateCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherCreateCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.CompletedTask;
        }
    }
}