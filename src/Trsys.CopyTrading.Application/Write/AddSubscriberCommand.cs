using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write
{
    public class AddSubscriberCommand : Command<DistributionGroupAggregate, DistributionGroupId>
    {
        public AddSubscriberCommand(DistributionGroupId aggregateId, AccountId accountId, TradeQuantity quantity) : base(aggregateId)
        {
            AccountId = accountId;
            Quantity = quantity;
        }

        public AccountId AccountId { get; }
        public TradeQuantity Quantity { get; }
    }

    public class AddSubscriberCommandHandler : CommandHandler<DistributionGroupAggregate, DistributionGroupId, AddSubscriberCommand>
    {
        public override Task ExecuteAsync(DistributionGroupAggregate aggregate, AddSubscriberCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddSubscriber(command.AccountId, command.Quantity);
            return Task.CompletedTask;
        }
    }
}
