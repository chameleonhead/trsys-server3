using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Commands
{
    public class PublisherEaRegisterCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaRegisterCommand(PublisherEaId aggregateId, SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
            PublisherId = publisherId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public PublisherId PublisherId { get; }
    }

    public class PublisherEaRegisterCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, PublisherEaRegisterCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, PublisherEaRegisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId, command.PublisherId);
            return Task.CompletedTask;
        }
    }
}
