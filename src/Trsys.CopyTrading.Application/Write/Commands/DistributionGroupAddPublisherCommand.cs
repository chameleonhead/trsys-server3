using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class DistributionGroupAddPublisherCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupAddPublisherCommand(DistributionGroupId aggregateId, PublisherId publisherId) : base(aggregateId)
        {
            PublisherId = publisherId;
        }

        public PublisherId PublisherId { get; }
    }

    public class DistributionGroupAddPublisherCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupAddPublisherCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupAddPublisherCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddPublisher(command.PublisherId);
            return Task.CompletedTask;
        }
    }
}
