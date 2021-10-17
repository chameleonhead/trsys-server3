using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublisherEaUpdateOrdersCommand : Command<PublisherEaAggregate, PublisherEaId>
    {
        public PublisherEaUpdateOrdersCommand(PublisherEaId aggregateId, EaOrderText text) : base(aggregateId)
        {
            Text = text;
        }

        public EaOrderText Text { get; }
    }

    public class PublisherEaUpdateOrdersCommandHandler : CommandHandler<PublisherEaAggregate, PublisherEaId, PublisherEaUpdateOrdersCommand>
    {
        public override Task ExecuteAsync(PublisherEaAggregate aggregate, PublisherEaUpdateOrdersCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateOrderText(command.Text);
            return Task.CompletedTask;
        }
    }
}
