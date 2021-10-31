using EventFlow;
using EventFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading;
using Trsys.CopyTrading.Abstractions;
using Trsys.Ea.Application;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Tests
{
    [TestClass]
    public class EaServiceTests
    {
        [TestMethod]
        public async Task AddSecretKeyAsync_Success_Publisher()
        {
            using var services = new ServiceCollection()
                .AddCopyTrading()
                .AddEa()
                .BuildServiceProvider();
            var service = services.GetRequiredService<IEaService>();
            await service.AddSecretKeyAsync("PublisherKey", "Publisher");
        }
    }
}
