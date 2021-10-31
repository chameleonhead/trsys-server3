using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupRemoveSubscriberCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupRemoveSubscriberCommand(DistributionGroupId aggregateId, SubscriberId subscriberId) : base(aggregateId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }

    public class DistributionGroupRemoveSubscriberCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupRemoveSubscriberCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupRemoveSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemvoeSubscriber(command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
