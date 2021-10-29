using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class PublisherDeleteCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherDeleteCommand(PublisherId aggregateId) : base(aggregateId)
        {
        }
    }

    public class PublisherDeleteCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherDeleteCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherDeleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.Delete();
            return Task.CompletedTask;
        }
    }
}