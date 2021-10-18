using EventFlow;
using EventFlow.Configuration;
using EventFlow.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Tests
{
    [TestClass]
    public class RegisterPublisherEaCommandTests
    {
        [TestMethod]
        public async Task Success()
        {
            using var resolver = CreateResolver();
            var commandBus = resolver.Resolve<ICommandBus>();

            var publisherEaId = PublisherEaId.New;

            var distributionGroupId = DistributionGroupId.New;
            var publisherId = PublisherId.New;

            var result = await commandBus.PublishAsync(new RegisterPublisherEaCommand(publisherEaId, new SecretKey("PublisherKey"), distributionGroupId, publisherId), CancellationToken.None);
            Assert.IsTrue(result.IsSuccess);

            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var queryResult = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(distributionGroupId.Value), CancellationToken.None);
            Assert.IsTrue(queryResult.Publishers.Contains(publisherId.Value));
        }

        private static IRootResolver CreateResolver()
        {
            return EventFlowOptions
                .New
                .UseApplication()
                .CreateResolver();
        }
    }
}
