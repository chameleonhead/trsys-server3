using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AddSubscriberCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public AddSubscriberCommand(DistributionGroupId aggregateId, AccountId accountId) : base(aggregateId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }

    public class AddSubscriberCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, AddSubscriberCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, AddSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddSubscriber(command.AccountId);
            return Task.CompletedTask;
        }
    }
}
