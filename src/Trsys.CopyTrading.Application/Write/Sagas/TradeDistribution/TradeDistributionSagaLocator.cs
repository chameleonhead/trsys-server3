using EventFlow.Aggregates;
using EventFlow.Sagas;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution
{
    public class TradeDistributionSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var tradeId = domainEvent.Metadata["copy-trade-id"];
            var sagaId = new TradeDistributionSagaId($"tradedistributionsaga-{tradeId}");
            return Task.FromResult<ISagaId>(sagaId);
        }
    }
}