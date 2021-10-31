using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Commands
{
    public class SubscriberEaRegisterCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaRegisterCommand(SubscriberEaId aggregateId, SecretKey key, DistributionGroupId distributionGroupId, SubscriberId subscriberId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            SubscriberId = subscriberId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public SubscriberId SubscriberId { get; }
    }

    public class SubscriberEaRegisterCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisterCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaRegisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId, command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
