using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.Core;

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
            var subscriberId = SubscriberId.New.ToString();
            await service.AddSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            var distributionGroup = await service.FindDistributionGroupByIdAsync(distributionGroupId, CancellationToken.None);
            CollectionAssert.AreEquivalent(distributionGroup.Subscribers, new[] { subscriberId });
        }

        [TestMethod]
        public async Task RemvoeSubscriberAsync_Success_SingleSubscriber()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriberId = SubscriberId.New.ToString();
            await service.AddSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            await service.RemoveSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            var distributionGroup = await service.FindDistributionGroupByIdAsync(distributionGroupId, CancellationToken.None);
            Assert.IsNull(distributionGroup);
        }

        [TestMethod]
        public async Task RemvoeSubscriberAsync_Success_MultipleSubscribers()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriberId1 = SubscriberId.New.ToString();
            var subscriberId2 = SubscriberId.New.ToString();
            await service.AddSubscriberAsync(distributionGroupId, subscriberId1, CancellationToken.None);
            await service.AddSubscriberAsync(distributionGroupId, subscriberId2, CancellationToken.None);
            await service.RemoveSubscriberAsync(distributionGroupId, subscriberId1, CancellationToken.None);
            var distributionGroup = await service.FindDistributionGroupByIdAsync(distributionGroupId, CancellationToken.None);
            CollectionAssert.AreEquivalent(distributionGroup.Subscribers, new[] { subscriberId2 });
        }

        [TestMethod]
        public async Task PublishOpenTradeAsync_Success_SingleSubscriber()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriberId = SubscriberId.New.ToString();
            var copyTradeId = CopyTradeId.New.ToString();
            await service.AddSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            await service.PublishOpenTradeAsync(distributionGroupId, copyTradeId, "USDJPY", "BUY", CancellationToken.None);
            var copyTrade = await service.FindCopyTradeByIdAsync(copyTradeId, CancellationToken.None);
            Assert.AreEqual("USDJPY", copyTrade.Symbol);
            Assert.AreEqual("BUY", copyTrade.OrderType);
        }

        [TestMethod]
        public async Task PublishOpenTradeAsync_Success_MultipleSubscribers()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscribers = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                var subscriberId = SubscriberId.New.ToString();
                subscribers.Add(subscriberId);
                await service.AddSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            }
            var copyTradeId = CopyTradeId.New.ToString();
            await service.PublishOpenTradeAsync(distributionGroupId, copyTradeId, "USDJPY", "BUY", CancellationToken.None);
            var copyTrade = await service.FindCopyTradeByIdAsync(copyTradeId, CancellationToken.None);
            Assert.AreEqual("USDJPY", copyTrade.Symbol);
            Assert.AreEqual("BUY", copyTrade.OrderType);
            Assert.IsTrue(copyTrade.IsOpen);
            CollectionAssert.AreEquivalent(subscribers, copyTrade.Subscribers);
        }

        [TestMethod]
        public async Task PublishCloseTradeAsync_Success_SingleSubscriber()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscriberId = SubscriberId.New.ToString();
            var copyTradeId = CopyTradeId.New.ToString();
            await service.AddSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            await service.PublishOpenTradeAsync(distributionGroupId, copyTradeId, "USDJPY", "BUY", CancellationToken.None);
            await service.PublishCloseTradeAsync(distributionGroupId, copyTradeId, CancellationToken.None);
            var copyTrade = await service.FindCopyTradeByIdAsync(copyTradeId, CancellationToken.None);
            Assert.IsFalse(copyTrade.IsOpen);
        }

        [TestMethod]
        public async Task PublishCloseTradeAsync_Success_MultipleSubscribers()
        {
            using var services = new ServiceCollection().AddCopyTrading().BuildServiceProvider();
            var service = services.GetRequiredService<ICopyTradingService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            var subscribers = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                var subscriberId = SubscriberId.New.ToString();
                subscribers.Add(subscriberId);
                await service.AddSubscriberAsync(distributionGroupId, subscriberId, CancellationToken.None);
            }
            var copyTradeId = CopyTradeId.New.ToString();
            await service.PublishOpenTradeAsync(distributionGroupId, copyTradeId, "USDJPY", "BUY", CancellationToken.None);
            await service.PublishCloseTradeAsync(distributionGroupId, copyTradeId, CancellationToken.None);
            var copyTrade = await service.FindCopyTradeByIdAsync(copyTradeId, CancellationToken.None);
            Assert.IsFalse(copyTrade.IsOpen);
        }
    }
}
