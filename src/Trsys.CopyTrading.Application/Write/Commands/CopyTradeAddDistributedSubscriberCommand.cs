using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class CopyTradeAddDistributedSubscriberCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeAddDistributedSubscriberCommand(CopyTradeId aggregateId, SubscriberId subscriberId) : base(aggregateId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }

    public class CopyTradeAddDistributedSubscriberCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, CopyTradeAddDistributedSubscriberCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, CopyTradeAddDistributedSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddDistributedSubscriber(command.SubscriberId);
            return Task.CompletedTask;
        }
    }
}