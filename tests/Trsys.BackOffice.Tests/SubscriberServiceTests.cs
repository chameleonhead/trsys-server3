using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;

namespace Trsys.BackOffice.Tests
{
    [TestClass]
    public class SubscriberServiceTests
    {
        [TestMethod]
        public async Task CreateAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<ISubscriberService>();
            var subscriberId = await service.CreateAsync("name", "description", CancellationToken.None);
            var subscriber = await service.FindByIdAsync(subscriberId, CancellationToken.None);
            Assert.AreEqual("name", subscriber.Name);
            Assert.AreEqual("description", subscriber.Description);
        }

        [TestMethod]
        public async Task UpdateNameAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<ISubscriberService>();
            var subscriberId = await service.CreateAsync("name", "description", CancellationToken.None);
            await service.UpdateNameAsync(subscriberId, "new-name", CancellationToken.None);
            var publisher = await service.FindByIdAsync(subscriberId, CancellationToken.None);
            Assert.AreEqual("new-name", publisher.Name);
        }

        [TestMethod]
        public async Task UpdateDescriptionAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<ISubscriberService>();
            var subscriberId = await service.CreateAsync("name", "description", CancellationToken.None);
            await service.UpdateDescriptionAsync(subscriberId, "new-description", CancellationToken.None);
            var publisher = await service.FindByIdAsync(subscriberId, CancellationToken.None);
            Assert.AreEqual("new-description", publisher.Description);
        }

        [TestMethod]
        public async Task DeleteAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<ISubscriberService>();
            var subscriberId = await service.CreateAsync("name", "description", CancellationToken.None);
            await service.DeleteAsync(subscriberId, CancellationToken.None);
            var subscriber = await service.FindByIdAsync(subscriberId, CancellationToken.None);
            Assert.IsNull(subscriber);
        }
    }
}
