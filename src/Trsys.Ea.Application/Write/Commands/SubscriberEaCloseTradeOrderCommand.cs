using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Commands
{
    public class SubscriberEaCloseTradeOrderCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaCloseTradeOrderCommand(SubscriberEaId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class SubscriberEaCloseTradeOrderCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaCloseTradeOrderCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaCloseTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}
