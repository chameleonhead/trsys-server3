using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class PublisherUpdateNameCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherUpdateNameCommand(PublisherId aggregateId, PublisherName name) : base(aggregateId)
        {
            Name = name;
        }

        public PublisherName Name { get; }
    }

    public class PublisherUpdateNameCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherUpdateNameCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherUpdateNameCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.CompletedTask;
        }
    }
}
