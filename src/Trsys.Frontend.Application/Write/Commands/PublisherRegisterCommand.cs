using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class PublisherRegisterCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherRegisterCommand(PublisherId aggregateId, SecretKey key, DistributionGroupId distributionGroupId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
    }

    public class PublisherRegisterCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherRegisterCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherRegisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId);
            return Task.CompletedTask;
        }
    }
}
