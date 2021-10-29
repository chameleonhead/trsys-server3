using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class DistributionGroupReadModelSearchCountQuery : IQuery<int>
    {
    }

    public class DistributionGroupReadModelSearchCountQueryHandler : IQueryHandler<DistributionGroupReadModelSearchCountQuery, int>
    {
        private readonly IInMemoryReadStore<DistributionGroupReadModel> readStore;

        public DistributionGroupReadModelSearchCountQueryHandler(IInMemoryReadStore<DistributionGroupReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<int> ExecuteQueryAsync(DistributionGroupReadModelSearchCountQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.Count;
        }
    }
}