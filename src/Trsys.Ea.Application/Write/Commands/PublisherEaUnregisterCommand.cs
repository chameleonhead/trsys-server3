using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Commands
{
    public class PublisherEaUnregisterCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaUnregisterCommand(PublisherEaId aggregateId, DistributionGroupId distributionGroupId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }

    public class PublisherEaUnregisterCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisterCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, PublisherEaUnregisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId);
            return Task.CompletedTask;
        }
    }
}
