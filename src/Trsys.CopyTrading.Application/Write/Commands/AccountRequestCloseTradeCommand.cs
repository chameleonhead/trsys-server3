using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountRequestCloseTradeOrderCommand : Command<AccountAggregate, AccountId>
    {
        public AccountRequestCloseTradeOrderCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class AccountRequestCloseTradeOrderCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountRequestCloseTradeOrderCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountRequestCloseTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.RequestCloseTradeOrder(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}