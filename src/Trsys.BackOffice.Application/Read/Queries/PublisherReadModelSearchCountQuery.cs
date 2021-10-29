using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class PublisherReadModelSearchCountQuery : IQuery<int>
    {
    }

    public class PublisherReadModelSearchCountQueryHandler : IQueryHandler<PublisherReadModelSearchCountQuery, int>
    {
        private readonly IInMemoryReadStore<PublisherReadModel> readStore;

        public PublisherReadModelSearchCountQueryHandler(IInMemoryReadStore<PublisherReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<int> ExecuteQueryAsync(PublisherReadModelSearchCountQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.Count;
        }
    }
}