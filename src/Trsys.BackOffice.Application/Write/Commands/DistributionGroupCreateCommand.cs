using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class DistributionGroupCreateCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupCreateCommand(DistributionGroupId aggregateId, DistributionGroupDisplayName displayName) : base(aggregateId)
        {
            DisplayName = displayName;
        }

        public DistributionGroupDisplayName DisplayName { get; }
    }

    public class DistributionGroupCreateCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupCreateCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetDisplayName(command.DisplayName);
            return Task.CompletedTask;
        }
    }
}