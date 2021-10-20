using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountDistributeCloseTradeOrderRequestCommand : Command<AccountAggregate, AccountId>
    {
        public AccountDistributeCloseTradeOrderRequestCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class AccountDistributeCloseTradeOrderRequestCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountDistributeCloseTradeOrderRequestCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountDistributeCloseTradeOrderRequestCommand command, CancellationToken cancellationToken)
        {
            aggregate.DistributeCloseTradeOrderRequest(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}