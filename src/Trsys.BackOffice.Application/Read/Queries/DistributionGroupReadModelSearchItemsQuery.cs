using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class DistributionGroupReadModelSearchItemsQuery : IQuery<List<DistributionGroupReadModel>>
    {
        public DistributionGroupReadModelSearchItemsQuery(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int Page { get; }
        public int PerPage { get; }
    }

    public class DistributionGroupReadModelSearchItemsQueryHandler : IQueryHandler<DistributionGroupReadModelSearchItemsQuery, List<DistributionGroupReadModel>>
    {
        private readonly IInMemoryReadStore<DistributionGroupReadModel> readStore;

        public DistributionGroupReadModelSearchItemsQueryHandler(IInMemoryReadStore<DistributionGroupReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<List<DistributionGroupReadModel>> ExecuteQueryAsync(DistributionGroupReadModelSearchItemsQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.ToList();
        }
    }
}