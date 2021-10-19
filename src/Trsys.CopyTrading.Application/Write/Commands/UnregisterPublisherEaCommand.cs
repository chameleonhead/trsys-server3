using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class UnregisterPublisherEaCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public UnregisterPublisherEaCommand(PublisherEaId aggregateId, DistributionGroupId distributionGroupId, PublisherId publisherId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }

    public class UnregisterPublisherEaCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, UnregisterPublisherEaCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, UnregisterPublisherEaCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId, command.PublisherId);
            return Task.CompletedTask;
        }
    }
}
