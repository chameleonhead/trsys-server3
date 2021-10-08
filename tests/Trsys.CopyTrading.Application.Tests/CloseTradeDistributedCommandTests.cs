using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Configuration;
using EventFlow.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Read.Queries;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Tests
{
    [TestClass]
    public class CloseTradeDistributedCommandTests
    {
        [TestMethod]
        public async Task SuccessWithASubscriber()
        {
            using var resolver = CreateResolver();
            var bus = resolver.Resolve<ICommandBus>();

            var accountId = AccountId.New;
            var distributionGroupId = DistributionGroupId.New;
            var result = await bus.PublishAsync(new AddSubscriberCommand(distributionGroupId, accountId), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
            var copyTradeId = CopyTradeId.New;
            result = await bus.PublishAsync(new PublishOrderOpenCommand(copyTradeId, distributionGroupId, ForexTradeSymbol.ValueOf("USDJPY"), OrderType.Buy), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
            result = await bus.PublishAsync(new DistributeOpenTradeCommand(accountId, copyTradeId), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
            result = await bus.PublishAsync(new DistributeCloseTradeCommand(accountId, copyTradeId), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);

            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var queryResult = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CopyTradeReadModel>(copyTradeId), CancellationToken.None);
            Assert.AreEqual(distributionGroupId.Value, queryResult.DistributionGroupId);
            Assert.AreEqual("USDJPY", queryResult.Symbol);
            Assert.AreEqual("BUY", queryResult.OrderType);
            Assert.IsTrue(queryResult.IsOpen);
        }

        [TestMethod]
        public async Task SuccessWithMultipleSubscribers()
        {
            var result = default(IExecutionResult);
            using var resolver = CreateResolver();
            var bus = resolver.Resolve<ICommandBus>();

            var distributionGroupId = DistributionGroupId.New;
            var accounts = Enumerable.Range(0, 100).Select(_ => AccountId.New).ToArray();
            foreach (var accountId in accounts)
            {
                result = await bus.PublishAsync(new AddSubscriberCommand(distributionGroupId, accountId), CancellationToken.None);
                Assert.IsTrue(result.IsSuccess);
            }
            var copyTradeId = CopyTradeId.New;
            result = await bus.PublishAsync(new PublishOrderOpenCommand(copyTradeId, distributionGroupId, ForexTradeSymbol.ValueOf("USDJPY"), OrderType.Buy), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);

            foreach (var accountId in accounts)
            {
                result = await bus.PublishAsync(new DistributeOpenTradeCommand(accountId, copyTradeId), CancellationToken.None);
                Assert.IsTrue(result.IsSuccess);
            }
            foreach (var accountId in accounts)
            {
                result = await bus.PublishAsync(new DistributeCloseTradeCommand(accountId, copyTradeId), CancellationToken.None);
                Assert.IsTrue(result.IsSuccess);
            }

            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var queryResult = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CopyTradeReadModel>(copyTradeId), CancellationToken.None);
            Assert.AreEqual(distributionGroupId.Value, queryResult.DistributionGroupId);
            Assert.AreEqual("USDJPY", queryResult.Symbol);
            Assert.AreEqual("BUY", queryResult.OrderType);
            Assert.IsTrue(queryResult.IsOpen);

            var queryResult2 = await queryProcessor.ProcessAsync(new TradeOrderReadModelAllQuery(), CancellationToken.None);
            Assert.AreEqual(100, queryResult2.Count);
            Assert.IsTrue(queryResult2.All(e => e.IsCloseDistributed));
        }

        private static IRootResolver CreateResolver()
        {
            return EventFlowOptions
                .New
                .UseApplication()
                .CreateResolver();
        }
    }
}
