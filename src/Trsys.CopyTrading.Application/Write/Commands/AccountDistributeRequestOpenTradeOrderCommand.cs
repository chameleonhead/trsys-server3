using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountDistributeRequestOpenTradeOrderCommand : Command<AccountAggregate, AccountId>
    {
        public AccountDistributeRequestOpenTradeOrderCommand(AccountId aggregateId, CopyTradeId copyTradeId) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }

    public class AccountDistributeRequestOpenTradeOrderCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountDistributeRequestOpenTradeOrderCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountDistributeRequestOpenTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.DistributeRequestOpenTradeOrder(command.CopyTradeId);
            return Task.CompletedTask;
        }
    }
}