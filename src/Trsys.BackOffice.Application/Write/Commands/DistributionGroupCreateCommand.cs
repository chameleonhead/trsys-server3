using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;
using Trsys.Core;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class DistributionGroupCreateCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupCreateCommand(DistributionGroupId aggregateId, DistributionGroupName name) : base(aggregateId)
        {
            Name = name;
        }

        public DistributionGroupName Name { get; }
    }

    public class DistributionGroupCreateCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupCreateCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetName(command.Name);
            return Task.CompletedTask;
        }
    }
}