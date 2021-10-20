using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublisherEaUnregisterCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaUnregisterCommand(PublisherEaId aggregateId, DistributionGroupId distributionGroupId, PublisherId publisherId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }

    public class PublisherEaUnregisterCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisterCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, PublisherEaUnregisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId, command.PublisherId);
            return Task.CompletedTask;
        }
    }
}
