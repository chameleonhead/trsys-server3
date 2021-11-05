using EventFlow.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Analytics.Domain;
using Trsys.Core;

namespace Trsys.Analytics.Application.Write.Commands
{
    public class PublisherOpenCopyTradeCommand : Command<PublisherAggregate, PublisherId>
    {
        public PublisherOpenCopyTradeCommand(PublisherId aggregateId, CopyTradeId copyTradeId, DateTimeOffset timestamp, ForexTradeSymbol symbol, OrderType orderType, Price price, Lot lots) : base(aggregateId)
        {
            CopyTradeId = copyTradeId;
            Timestamp = timestamp;
            Symbol = symbol;
            OrderType = orderType;
            Price = price;
            Lots = lots;
        }

        public CopyTradeId CopyTradeId { get; }
        public DateTimeOffset Timestamp { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public Price Price { get; }
        public Lot Lots { get; }
    }

    public class PublisherOpenCopyTradeCommandHandler : CommandHandler<PublisherAggregate, PublisherId, PublisherOpenCopyTradeCommand>
    {
        public override Task ExecuteAsync(PublisherAggregate aggregate, PublisherOpenCopyTradeCommand command, CancellationToken cancellationToken)
        {
            aggregate.Open(command.CopyTradeId, command.Timestamp, command.Symbol, command.OrderType, command.Price, command.Lots);
            return Task.CompletedTask;
        }
    }

}
