using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class PublisherEaRegisterCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaRegisterCommand(PublisherEaId aggregateId, SecretKey key, DistributionGroupId distributionGroupId) : base(aggregateId)
        {
            Key = key;
            DistributionGroupId = distributionGroupId;
        }

        public SecretKey Key { get; }
        public DistributionGroupId DistributionGroupId { get; }
    }

    public class PublisherEaRegisterCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, PublisherEaRegisterCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, PublisherEaRegisterCommand command, CancellationToken cancellationToken)
        {
            aggregate.Register(command.Key, command.DistributionGroupId);
            return Task.CompletedTask;
        }
    }
}
