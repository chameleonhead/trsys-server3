using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class RegisterPublisherSecretKeyCommand : Command<SecretKeyAggregate, SecretKeyId>
    {
        public RegisterPublisherSecretKeyCommand(SecretKeyId aggregateId, SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }

    public class RegisterPublisherSecretKeyCommandHandler : CommandHandler<SecretKeyAggregate, SecretKeyId, RegisterPublisherSecretKeyCommand>
    {
        public override Task ExecuteAsync(SecretKeyAggregate aggregate, RegisterPublisherSecretKeyCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId, command.PublisherId);
            return Task.CompletedTask;
        }
    }
}
