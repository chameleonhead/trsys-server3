using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupAddSubscriberCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupAddSubscriberCommand(DistributionGroupId aggregateId, SubscriberId accountId) : base(aggregateId)
        {
            AccountId = accountId;
        }

        public SubscriberId AccountId { get; }
    }

    public class DistributionGroupAddSubscriberCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupAddSubscriberCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupAddSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddSubscriber(command.AccountId);
            return Task.CompletedTask;
        }
    }
}
