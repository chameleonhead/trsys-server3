using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Trsys.CopyTrading;
using Trsys.Core;
using Trsys.Frontend.Abstractions;

namespace Trsys.Frontend.Tests
{
    [TestClass]
    public class EaServiceTests
    {
        [TestMethod]
        public async Task AddSecretKeyAsync_Success_Publisher()
        {
            using var services = new ServiceCollection()
                .AddCopyTrading()
                .AddFrontend()
                .BuildServiceProvider();
            var service = services.GetRequiredService<IEaService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            await service.AddSecretKeyAsync(distributionGroupId, "PublisherKey", "Publisher");
            var secretKey = await service.FindByKeyAsync("PublisherKey", "Publisher");
            Assert.AreEqual("PublisherKey", secretKey.Key);
            Assert.AreEqual("Publisher", secretKey.KeyType);
        }

        [TestMethod]
        public async Task AddSecretKeyAsync_Success_Subscriber()
        {
            using var services = new ServiceCollection()
                .AddCopyTrading()
                .AddFrontend()
                .BuildServiceProvider();
            var service = services.GetRequiredService<IEaService>();
            var distributionGroupId = DistributionGroupId.New.ToString();
            await service.AddSecretKeyAsync(distributionGroupId, "SubscriberKey", "Subscriber");
            var secretKey = await service.FindByKeyAsync("SubscriberKey", "Subscriber");
            Assert.AreEqual("SubscriberKey", secretKey.Key);
            Assert.AreEqual("Subscriber", secretKey.KeyType);
        }
    }
}
