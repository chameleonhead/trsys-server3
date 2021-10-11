using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class PublishOrderCloseCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public PublishOrderCloseCommand(CopyTradeId aggregateId, PublisherIdentifier clientKey) : base(aggregateId)
        {
            ClientKey = clientKey;
        }

        public PublisherIdentifier ClientKey { get; }
    }

    public class PublishOrderCloseCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, PublishOrderCloseCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, PublishOrderCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.ClientKey);
            return Task.CompletedTask;
        }
    }
}
