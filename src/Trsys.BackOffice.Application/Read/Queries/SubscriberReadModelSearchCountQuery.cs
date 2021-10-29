using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class SubscriberReadModelSearchCountQuery : IQuery<int>
    {
    }

    public class SubscriberReadModelSearchCountQueryHandler : IQueryHandler<SubscriberReadModelSearchCountQuery, int>
    {
        private readonly IInMemoryReadStore<SubscriberReadModel> readStore;

        public SubscriberReadModelSearchCountQueryHandler(IInMemoryReadStore<SubscriberReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<int> ExecuteQueryAsync(SubscriberReadModelSearchCountQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.Count;
        }
    }
}