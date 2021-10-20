using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountDistributeOpenTradeOrderRequestCommand : Command<AccountAggregate, AccountId>
    {
        public AccountDistributeOpenTradeOrderRequestCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class AccountDistributeOpenTradeOrderRequestCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountDistributeOpenTradeOrderRequestCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountDistributeOpenTradeOrderRequestCommand command, CancellationToken cancellationToken)
        {
            aggregate.DistributeOpenTradeOrderRequest(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}