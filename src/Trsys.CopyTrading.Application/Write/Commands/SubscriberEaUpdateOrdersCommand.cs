using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class SubscriberEaUpdateOrdersCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaUpdateOrdersCommand(SubscriberEaId aggregateId, EaOrderText text) : base(aggregateId)
        {
            Text = text;
        }

        public EaOrderText Text { get; }
    }

    public class SubscriberEaUpdateOrdersCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUpdateOrdersCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaUpdateOrdersCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateOrderText(command.Text);
            return Task.CompletedTask;
        }
    }
}
