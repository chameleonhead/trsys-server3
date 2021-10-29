using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class SubscriberReadModelSearchItemsQuery : IQuery<List<SubscriberReadModel>>
    {
        public SubscriberReadModelSearchItemsQuery(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int Page { get; }
        public int PerPage { get; }
    }

    public class SubscriberReadModelSearchItemsQueryHandler : IQueryHandler<SubscriberReadModelSearchItemsQuery, List<SubscriberReadModel>>
    {
        private readonly IInMemoryReadStore<SubscriberReadModel> readStore;

        public SubscriberReadModelSearchItemsQueryHandler(IInMemoryReadStore<SubscriberReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<List<SubscriberReadModel>> ExecuteQueryAsync(SubscriberReadModelSearchItemsQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.ToList();
        }
    }
}