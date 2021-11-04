using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Trsys.Analytics.Abstractions;
using Trsys.Core;

namespace Trsys.Analytics.Tests
{
    [TestClass]
    public class AnalyticsServiceTests
    {
        [TestMethod]
        public async Task OpenCopyTradeAsync_Success()
        {
            using var services = new ServiceCollection().AddAnalytics().BuildServiceProvider();
            var service = services.GetRequiredService<IAnalyticsService>();
            var copyTradeId = CopyTradeId.New.Value;
            await service.OpenCopyTradeAsync(copyTradeId, DateTimeOffset.Parse("2021-11-05T01:58:00.000Z"), "USDJPY", "BUY");
        }
    }
}
