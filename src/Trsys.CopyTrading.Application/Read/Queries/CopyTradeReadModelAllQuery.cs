using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;

namespace Trsys.CopyTrading.Application.Read.Queries
{
    public class CopyTradeReadModelAllQuery : IQuery<IReadOnlyCollection<CopyTradeReadModel>>
    {
    }

    public class CopyTradeReadModelAllQueryHandler : IQueryHandler<CopyTradeReadModelAllQuery, IReadOnlyCollection<CopyTradeReadModel>>
    {
        private readonly IInMemoryReadStore<CopyTradeReadModel> _readStore;

        public CopyTradeReadModelAllQueryHandler(IInMemoryReadStore<CopyTradeReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<CopyTradeReadModel>> ExecuteQueryAsync(CopyTradeReadModelAllQuery query, CancellationToken cancellationToken)
        {
            var result = await _readStore.FindAsync(_ => true, cancellationToken).ConfigureAwait(false);
            return result;
        }
    }
}
