using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

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
