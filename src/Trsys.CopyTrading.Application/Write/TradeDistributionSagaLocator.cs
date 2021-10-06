using EventFlow.Aggregates;
using EventFlow.Sagas;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading.Application.Write
{
    public class TradeDistributionSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return Task.FromResult<ISagaId>(TradeDistributionId.New);
        }
    }
}