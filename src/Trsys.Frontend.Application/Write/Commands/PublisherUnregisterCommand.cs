using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class PublisherUnregisterCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherUnregisterCommand(PublisherId aggregateId, DistributionGroupId distributionGroupId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }

    public class PublisherUnregisterCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherUnregisterCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherUnregisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId);
            return Task.CompletedTask;
        }
    }
}
