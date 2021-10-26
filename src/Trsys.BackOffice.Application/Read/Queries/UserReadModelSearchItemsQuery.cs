using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class UserReadModelSearchItemsQuery : IQuery<List<UserReadModel>>
    {
        public UserReadModelSearchItemsQuery(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int Page { get; }
        public int PerPage { get; }
    }

    public class UserReadModelSearchItemsQueryHandler : IQueryHandler<UserReadModelSearchItemsQuery, List<UserReadModel>>
    {
        private readonly IInMemoryReadStore<UserReadModel> readStore;

        public UserReadModelSearchItemsQueryHandler(IInMemoryReadStore<UserReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<List<UserReadModel>> ExecuteQueryAsync(UserReadModelSearchItemsQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.ToList();
        }
    }
}