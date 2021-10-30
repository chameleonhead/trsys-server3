using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupRemoveSubscriberCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupRemoveSubscriberCommand(DistributionGroupId aggregateId, AccountId subscriberId) : base(aggregateId)
        {
            AccountId = subscriberId;
        }

        public AccountId AccountId { get; }
    }

    public class DistributionGroupRemoveSubscriberCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupRemoveSubscriberCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupRemoveSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemvoeSubscriber(command.AccountId);
            return Task.CompletedTask;
        }
    }
}
