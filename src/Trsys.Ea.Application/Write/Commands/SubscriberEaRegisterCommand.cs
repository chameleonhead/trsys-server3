using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Commands
{
    public class SubscriberEaRegisterCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaRegisterCommand(SubscriberEaId aggregateId, SecretKey key, DistributionGroupId distributionGroupId, AccountId accountId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            AccountId = accountId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public AccountId AccountId { get; }
    }

    public class SubscriberEaRegisterCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisterCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaRegisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId, command.AccountId);
            return Task.CompletedTask;
        }
    }
}
