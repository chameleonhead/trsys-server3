using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice.Tests
{
    [TestClass]
    public class PublisherServiceTests
    {
        [TestMethod]
        public async Task CreateAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IPublisherService>();
            var publisherId = await service.CreateAsync("name", "description", CancellationToken.None);
            var publisher = await service.FindByIdAsync(publisherId, CancellationToken.None);
            Assert.AreEqual("name", publisher.Name);
            Assert.AreEqual("description", publisher.Description);
        }

        [TestMethod]
        public async Task DeleteAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IPublisherService>();
            var publisherId = await service.CreateAsync("name", "description", CancellationToken.None);
            await service.DeleteAsync(publisherId, CancellationToken.None);
            var publisher = await service.FindByIdAsync(publisherId, CancellationToken.None);
            Assert.IsNull(publisher);
        }
    }
}
