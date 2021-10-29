using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Write.Commands
{
    public class DistributionGroupDeleteCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupDeleteCommand(DistributionGroupId aggregateId) : base(aggregateId)
        {
        }
    }

    public class DistributionGroupDeleteCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDeleteCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, DistributionGroupDeleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.Delete();
            return Task.CompletedTask;
        }
    }
}