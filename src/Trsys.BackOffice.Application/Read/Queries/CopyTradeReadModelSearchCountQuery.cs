using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class CopyTradeReadModelSearchCountQuery : IQuery<int>
    {
    }

    public class CopyTradeReadModelSearchCountQueryHandler : IQueryHandler<CopyTradeReadModelSearchCountQuery, int>
    {
        private readonly IInMemoryReadStore<CopyTradeReadModel> readStore;

        public CopyTradeReadModelSearchCountQueryHandler(IInMemoryReadStore<CopyTradeReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<int> ExecuteQueryAsync(CopyTradeReadModelSearchCountQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.Count;
        }
    }
}