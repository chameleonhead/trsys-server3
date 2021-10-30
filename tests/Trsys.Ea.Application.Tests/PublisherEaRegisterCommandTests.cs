using EventFlow;
using EventFlow.Configuration;
using EventFlow.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Tests
{
    [TestClass]
    public class PublisherEaRegisterCommandTests
    {
        [TestMethod]
        public async Task Success()
        {
            using var resolver = CreateResolver();
            var commandBus = resolver.Resolve<ICommandBus>();

            var publisherEaId = PublisherEaId.New;

            var distributionGroupId = DistributionGroupId.New;

            var result = await commandBus.PublishAsync(new PublisherEaRegisterCommand(publisherEaId, new SecretKey("PublisherKey"), distributionGroupId), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);
        }

        private static IRootResolver CreateResolver()
        {
            return EventFlowOptions
                .New
                .UseCopyTradeApplication()
                .UseEaApplication()
                .CreateResolver();
        }
    }
}
