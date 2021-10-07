using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write
{
    public class PublishOrderCloseCommand : Command<CopyTradeAggregate, CopyTradeId>
    {
        public PublishOrderCloseCommand(CopyTradeId aggregateId) : base(aggregateId)
        {
        }
    }

    public class PublishOrderCloseCommandHandler : CommandHandler<CopyTradeAggregate, CopyTradeId, PublishOrderCloseCommand>
    {
        public override Task ExecuteAsync(CopyTradeAggregate aggregate, PublishOrderCloseCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close();
            return Task.CompletedTask;
        }
    }
}
