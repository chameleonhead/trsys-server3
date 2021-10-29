using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class PublisherReadModelSearchItemsQuery : IQuery<List<PublisherReadModel>>
    {
        public PublisherReadModelSearchItemsQuery(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int Page { get; }
        public int PerPage { get; }
    }

    public class PublisherReadModelSearchItemsQueryHandler : IQueryHandler<PublisherReadModelSearchItemsQuery, List<PublisherReadModel>>
    {
        private readonly IInMemoryReadStore<PublisherReadModel> readStore;

        public PublisherReadModelSearchItemsQueryHandler(IInMemoryReadStore<PublisherReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<List<PublisherReadModel>> ExecuteQueryAsync(PublisherReadModelSearchItemsQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.ToList();
        }
    }
}