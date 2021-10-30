using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupAddSubscriberCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupAddSubscriberCommand(DistributionGroupId aggregateId, SubscriberId subscriberId) : base(aggregateId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }

    public class DistributionGroupAddSubscriberCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupAddSubscriberCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupAddSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddSubscriber(command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
