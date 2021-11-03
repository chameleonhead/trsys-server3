using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberCloseTradeOrderCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberCloseTradeOrderCommand(SubscriberId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class SubscriberCloseTradeOrderCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberCloseTradeOrderCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberCloseTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}
