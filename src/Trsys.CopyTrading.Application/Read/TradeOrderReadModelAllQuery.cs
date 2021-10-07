using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading.Application.Read
{
    public class TradeOrderReadModelAllQuery : IQuery<IReadOnlyCollection<TradeOrderReadModel>>
    {
    }

    public class TradeOrderReadModelAllQueryHandler : IQueryHandler<TradeOrderReadModelAllQuery, IReadOnlyCollection<TradeOrderReadModel>>
    {
        private readonly IInMemoryReadStore<TradeOrderReadModel> _readStore;

        public TradeOrderReadModelAllQueryHandler(IInMemoryReadStore<TradeOrderReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<TradeOrderReadModel>> ExecuteQueryAsync(TradeOrderReadModelAllQuery query, CancellationToken cancellationToken)
        {
            var result = await _readStore.FindAsync(_ => true, cancellationToken).ConfigureAwait(false);
            return result;
        }
    }
}
