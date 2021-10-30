using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeRemoveDistributedAccountCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeRemoveDistributedAccountCommand(CopyTradeId aggregateId, SubscriberId subscriberId) : base(aggregateId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }

    public class CopyTradeRemoveDistributedAccountCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeRemoveDistributedAccountCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeRemoveDistributedAccountCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemoveApplicant(command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
