using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberUnregisterCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberUnregisterCommand(SubscriberId aggregateId, DistributionGroupId distributionGroupId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }

    public class SubscriberEaUnregisterCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberUnregisterCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberUnregisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId);
            return Task.CompletedTask;
        }
    }
}
