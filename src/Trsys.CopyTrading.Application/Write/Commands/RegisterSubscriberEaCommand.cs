using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class RegisterSubscriberEaCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public RegisterSubscriberEaCommand(SubscriberEaId aggregateId, SecretKey key, DistributionGroupId distributionGroupId, AccountId accountId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            AccountId = accountId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public AccountId AccountId { get; }
    }

    public class RegisterSubscriberEaCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, RegisterSubscriberEaCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, RegisterSubscriberEaCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId, command.AccountId);
            return Task.CompletedTask;
        }
    }
}
