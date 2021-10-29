using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice.Tests
{
    [TestClass]
    public class DistributionGroupServiceTests
    {
        [TestMethod]
        public async Task CreateAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IDistributionGroupService>();
            var distributionGroupId = await service.CreateAsync("name", CancellationToken.None);
            var distributionGroup = await service.FindByIdAsync(distributionGroupId, CancellationToken.None);
            Assert.AreEqual("name", distributionGroup.Name);
        }

        [TestMethod]
        public async Task DeleteAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IDistributionGroupService>();
            var distributionGroupId = await service.CreateAsync("name", CancellationToken.None);
            await service.DeleteAsync(distributionGroupId, CancellationToken.None);
            var distributionGroup = await service.FindByIdAsync(distributionGroupId, CancellationToken.None);
            Assert.IsNull(distributionGroup);
        }
    }
}
