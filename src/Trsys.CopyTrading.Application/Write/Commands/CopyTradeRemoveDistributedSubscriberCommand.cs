using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeRemoveDistributedSubscriberCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeRemoveDistributedSubscriberCommand(CopyTradeId aggregateId, SubscriberId subscriberId) : base(aggregateId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }

    public class CopyTradeRemoveDistributedAccountCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeRemoveDistributedSubscriberCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeRemoveDistributedSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemoveDistributedSubscriber(command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}
