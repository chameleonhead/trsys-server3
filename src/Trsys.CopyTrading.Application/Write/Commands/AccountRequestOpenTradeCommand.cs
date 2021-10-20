using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Commands
{
    public class AccountRequestOpenTradeOrderCommand : Command<AccountAggregate, AccountId>
    {
        public AccountRequestOpenTradeOrderCommand(AccountId aggregateId, CopyTradeId copyTradeId, DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public CopyTradeId CopyTradeId { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }

    public class AccountRequestOpenTradeOrderCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountRequestOpenTradeOrderCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, AccountRequestOpenTradeOrderCommand command, CancellationToken cancellationToken)
        {
            aggregate.RequestOpenTradeOrder(command.CopyTradeId, command.Symbol, command.OrderType);
            return Task.CompletedTask;
        }
    }
}