using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application.Write.Commands
{
    public class PublisherEaUpdateOrderTextCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaUpdateOrderTextCommand(PublisherEaId aggregateId, EaOrderText text) : base(aggregateId)
        {
            Text = text;
        }

        public EaOrderText Text { get; }
    }

    public class PublisherEaUpdateOrderTextCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, PublisherEaUpdateOrderTextCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, PublisherEaUpdateOrderTextCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateOrderText(command.Text);
            return Task.CompletedTask;
        }
    }
}
