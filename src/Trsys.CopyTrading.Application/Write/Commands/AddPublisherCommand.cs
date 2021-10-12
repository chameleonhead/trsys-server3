using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AddPublisherCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public AddPublisherCommand(DistributionGroupId aggregateId, ClientKey clientKey) : base(aggregateId)
        {
            ClientKey = clientKey;
        }

        public ClientKey ClientKey { get; }
    }

    public class AddPublisherCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, AddPublisherCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, AddPublisherCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddPublisher(command.ClientKey);
            return Task.CompletedTask;
        }
    }
}
