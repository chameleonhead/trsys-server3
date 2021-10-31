using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;
using Trsys.Core;

namespace Trsys.BackOffice.Tests
{
    [TestClass]
    public class CopyTradeServiceTests
    {
        [TestMethod]
        public async Task OpenAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradeService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var copyTradeId = await service.OpenAsync(distributionGroupId, "symbol", "BUY", CancellationToken.None);
            var copyTrade = await service.FindByIdAsync(copyTradeId, CancellationToken.None);
            Assert.AreEqual("symbol", copyTrade.Symbol);
            Assert.AreEqual("BUY", copyTrade.OrderType);
        }

        [TestMethod]
        public async Task CloseAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradeService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var copyTradeId = await service.OpenAsync(distributionGroupId, "symbol", "BUY", CancellationToken.None);
            await service.CloseAsync(copyTradeId, CancellationToken.None);
            var copyTrade = await service.FindByIdAsync(copyTradeId, CancellationToken.None);
            Assert.IsTrue(copyTrade.IsClosed);
        }
    }
}
