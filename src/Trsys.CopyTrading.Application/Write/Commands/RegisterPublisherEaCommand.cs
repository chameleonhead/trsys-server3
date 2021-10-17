using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class RegisterPublisherEaCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public RegisterPublisherEaCommand(PublisherEaId aggregateId, SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }

    public class RegisterPublisherSecretKeyCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, RegisterPublisherEaCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, RegisterPublisherEaCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId, command.PublisherId);
            return Task.CompletedTask;
        }
    }
}
