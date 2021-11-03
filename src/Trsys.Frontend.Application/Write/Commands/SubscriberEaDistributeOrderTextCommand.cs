using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberEaDistributeOrderTextCommand : Command<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaDistributeOrderTextCommand(SubscriberEaId aggregateId, EaOrderText text) : base(aggregateId)
        {
            Text = text;
        }

        public EaOrderText Text { get; }
    }

    public class SubscriberEaDistributeOrderTextCommandHandler : CommandHandler<SubscriberEaAggregate, SubscriberEaId, SubscriberEaDistributeOrderTextCommand>
    {
        public override Task ExecuteAsync(SubscriberEaAggregate aggregate, SubscriberEaDistributeOrderTextCommand command, CancellationToken cancellationToken)
        {
            aggregate.DistributeOrderText(command.Text);
            return Task.CompletedTask;
        }
    }
}
