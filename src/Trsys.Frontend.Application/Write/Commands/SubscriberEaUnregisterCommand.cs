using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberEaUnregisterCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaUnregisterCommand(SubscriberEaId aggregateId, DistributionGroupId distributionGroupId, SubscriberId subscriberId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            SubscriberId = subscriberId;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public SubscriberId SubscriberId { get; }
    }

    public class SubscriberEaUnregisterCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisterCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaUnregisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId, command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
