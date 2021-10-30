using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Tests
{
    [TestClass]
    public class CopyTradeServiceTests
    {
        [TestMethod]
        public async Task AddSubscriberAsync_Success()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriptionId = await service.AddSubscriberAsync(distributionGroupId, CancellationToken.None);
            var distributionGroup = await service.FindDistributionGroupByIdAsync(distributionGroupId, CancellationToken.None);
            CollectionAssert.AreEquivalent(distributionGroup.Subscribers, new[] { subscriptionId });
        }

        [TestMethod]
        public async Task RemvoeSubscriberAsync_Success_SingleSubscriber()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriptionId = await service.AddSubscriberAsync(distributionGroupId, CancellationToken.None);
            await service.RemoveSubscriberAsync(distributionGroupId, subscriptionId, CancellationToken.None);
            var distributionGroup = await service.FindDistributionGroupByIdAsync(distributionGroupId, CancellationToken.None);
            Assert.IsNull(distributionGroup);
        }

        [TestMethod]
        public async Task RemvoeSubscriberAsync_Success_MultipleSubscribers()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriptionId1 = await service.AddSubscriberAsync(distributionGroupId, CancellationToken.None);
            var subscriptionId2 = await service.AddSubscriberAsync(distributionGroupId, CancellationToken.None);
            await service.RemoveSubscriberAsync(distributionGroupId, subscriptionId1, CancellationToken.None);
            var distributionGroup = await service.FindDistributionGroupByIdAsync(distributionGroupId, CancellationToken.None);
            CollectionAssert.AreEquivalent(distributionGroup.Subscribers, new[] { subscriptionId2 });
        }

        [TestMethod]
        public async Task PublishOpenTradeAsync_Success_SingleSubscriber()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriptionId = await service.AddSubscriberAsync(distributionGroupId, CancellationToken.None);
            var copyTradeId = await service.PublishOpenTradeAsync(distributionGroupId, "USDJPY", "BUY", CancellationToken.None);
            var copyTrade = await service.FindCopyTradeByIdAsync(copyTradeId, CancellationToken.None);
            Assert.AreEqual("USDJPY", copyTrade.Symbol);
            Assert.AreEqual("BUY", copyTrade.OrderType);
        }
    }
}
