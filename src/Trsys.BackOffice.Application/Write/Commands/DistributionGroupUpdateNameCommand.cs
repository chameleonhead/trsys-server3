using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class DistributionGroupUpdateNameCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupUpdateNameCommand(DistributionGroupId aggregateId, DistributionGroupName name) : base(aggregateId)
        {
            Name = name;
        }

        public DistributionGroupName Name { get; }
    }

    public class DistributionGroupUpdateNameCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupUpdateNameCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupUpdateNameCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.CompletedTask;
        }
    }

}
