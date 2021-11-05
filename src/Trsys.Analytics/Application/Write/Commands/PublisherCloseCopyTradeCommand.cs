using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Analytics.Domain;
using Trsys.Core;

namespace Trsys.Analytics.Application.Write.Commands
{
    public class PublisherCloseCopyTradeCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherCloseCopyTradeCommand(PublisherId aggregateId, CopyTradeId copyTradeId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType, Price price, Lot lots, Profit profit) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Timestamp = timestamp;
            Symbol = symbol;
            OrderType = orderType;
            Price = price;
            Lots = lots;
            Profit = profit;
        }

        public CopyTradeId CopyTradeId { get; }
        public DateTimeOffset Timestamp { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public Price Price { get; }
        public Lot Lots { get; }
        public Profit Profit { get; }
    }

    public class PublisherCloseCopyTradeCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherCloseCopyTradeCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherCloseCopyTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.Close(command.CopyTradeId, command.Timestamp, command.Symbol, command.OrderType, command.Price, command.Lots, command.Profit);
            return Task.CompletedTask;
        }
    }

}
