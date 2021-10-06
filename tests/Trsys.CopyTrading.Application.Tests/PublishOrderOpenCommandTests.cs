using EventFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public async Task Success()
        {
            using var resolver = EventFlowOptions
                .New
                .UseApplication()
                .CreateResolver();
            var bus = resolver.Resolve<ICommandBus>();

            var distributionGroupId = DistributionGroupId.New;
            var result = await bus.PublishAsync(new PublishOrderOpenCommand(CopyTradeId.New, distributionGroupId, ForexTradeSymbol.ValueOf("USDJPY"), OrderType.Buy), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
