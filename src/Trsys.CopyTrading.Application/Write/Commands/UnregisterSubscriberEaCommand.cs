using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class UnregisterSubscriberEaCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public UnregisterSubscriberEaCommand(SubscriberEaId aggregateId, DistributionGroupId distributionGroupId, AccountId accountId) : base(aggregateId)
        {
            DistributionGroupId = distributionGroupId;
            AccountId = accountId;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public AccountId AccountId { get; }
    }

    public class UnregisterSubscriberEaCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, UnregisterSubscriberEaCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, UnregisterSubscriberEaCommand command, CancellationToken cancellationToken)
        {
            aggregate.Unregister(command.DistributionGroupId, command.AccountId);
            return Task.CompletedTask;
        }
    }
}
