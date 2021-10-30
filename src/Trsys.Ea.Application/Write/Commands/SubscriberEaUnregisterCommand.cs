using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Commands
{
    public class SubscriberEaUnregisterCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaUnregisterCommand(SubscriberEaId aggregateId, DistributionGroupId distributionGroupId, SubscriberId accountId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            AccountId = accountId;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public SubscriberId AccountId { get; }
    }

    public class SubscriberEaUnregisterCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisterCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaUnregisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId, command.AccountId);
            return Task.CompletedTask;
        }
    }
}
