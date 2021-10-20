using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountDistributeRequestCloseTradeOrderCommand : Command<AccountAggregate, AccountId>
    {
        public AccountDistributeRequestCloseTradeOrderCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class AccountDistributeRequestCloseTradeOrderCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountDistributeRequestCloseTradeOrderCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountDistributeRequestCloseTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.DistributeRequestCloseTradeOrder(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}