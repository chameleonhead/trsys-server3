using EventFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Tests
{
    [TestClass]
    public class PublishOrderOpenCommandTests
    {
        [TestMethod]
        public async Task SuccessWithoutSubscriber()
        {
            using var resolver = EventFlowOptions
                .New
                .UseApplication()
                .CreateResolver();
            var bus = resolver.Resolve<ICommandBus>();
            var unknownDistributionGroupId = DistributionGroupId.New;
            var result = await bus.PublishAsync(new PublishOrderOpenCommand(CopyTradeId.New, unknownDistributionGroupId, ForexTradeSymbol.ValueOf("USDJPY"), OrderType.Buy), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task SuccessWithASubscriber()
        {
            using var resolver = EventFlowOptions
                .New
                .UseApplication()
                .CreateResolver();
            var bus = resolver.Resolve<ICommandBus>();

            var accountId = AccountId.New;
            var knownDistributionGroupId = DistributionGroupId.New;
            var result = await bus.PublishAsync(new AddSubscriberCommand(knownDistributionGroupId, accountId, TradeQuantity.Percentage(98)), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
            result = await bus.PublishAsync(new PublishOrderOpenCommand(CopyTradeId.New, knownDistributionGroupId, ForexTradeSymbol.ValueOf("USDJPY"), OrderType.Buy), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
