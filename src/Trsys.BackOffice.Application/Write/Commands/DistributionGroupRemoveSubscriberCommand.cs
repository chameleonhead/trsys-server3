using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
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
            aggregate.RemoveSubscriber(command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
