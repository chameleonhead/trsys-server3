using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class SubscriberDistributeOrderTextCommand : Command<SubscriberAggregate, SubscriberId>
    {
        public SubscriberDistributeOrderTextCommand(SubscriberId aggregateId, EaOrderText text) : base(aggregateId)
        {
            Text = text;
        }

        public EaOrderText Text { get; }
    }

    public class SubscriberDistributeOrderTextCommandHandler : CommandHandler<SubscriberAggregate, SubscriberId, SubscriberDistributeOrderTextCommand>
    {
        public override Task ExecuteAsync(SubscriberAggregate aggregate, SubscriberDistributeOrderTextCommand command, CancellationToken cancellationToken)
        {
            aggregate.DistributeOrderText(command.Text);
            return Task.CompletedTask;
        }
    }
}
