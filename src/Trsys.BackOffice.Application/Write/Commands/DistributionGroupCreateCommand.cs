using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class DistributionGroupCreateCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupCreateCommand(DistributionGroupId aggregateId, DistributionGroupName distributionGroupName) : base(aggregateId)
        {
            DistributionGroupName = distributionGroupName;
        }

        public DistributionGroupName DistributionGroupName { get; }
    }

    public class DistributionGroupCreateCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupCreateCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.DistributionGroupName);
            return Task.CompletedTask;
        }
    }
}