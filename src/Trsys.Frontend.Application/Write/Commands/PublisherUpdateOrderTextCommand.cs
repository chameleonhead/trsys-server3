using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class PublisherUpdateOrderTextCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherUpdateOrderTextCommand(PublisherId aggregateId, EaOrderText text) : base(aggregateId)
        {
            Text = text;
        }

        public EaOrderText Text { get; }
    }

    public class PublisherUpdateOrderTextCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherUpdateOrderTextCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherUpdateOrderTextCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateOrderText(command.Text);
            return Task.CompletedTask;
        }
    }
}
