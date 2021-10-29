using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class CopyTradeReadModelSearchItemsQuery : IQuery<List<CopyTradeReadModel>>
    {
        public CopyTradeReadModelSearchItemsQuery(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int Page { get; }
        public int PerPage { get; }
    }

    public class CopyTradeReadModelSearchItemsQueryHandler : IQueryHandler<CopyTradeReadModelSearchItemsQuery, List<CopyTradeReadModel>>
    {
        private readonly IInMemoryReadStore<CopyTradeReadModel> readStore;

        public CopyTradeReadModelSearchItemsQueryHandler(IInMemoryReadStore<CopyTradeReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<List<CopyTradeReadModel>> ExecuteQueryAsync(CopyTradeReadModelSearchItemsQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.ToList();
        }
    }
}