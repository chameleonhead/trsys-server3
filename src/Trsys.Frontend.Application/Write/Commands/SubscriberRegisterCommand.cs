using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberRegisterCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberRegisterCommand(SubscriberId aggregateId, SecretKey key, DistributionGroupId distributionGroupId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
    }

    public class SubscriberRegisterCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberRegisterCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberRegisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId);
            return Task.CompletedTask;
        }
    }
}
