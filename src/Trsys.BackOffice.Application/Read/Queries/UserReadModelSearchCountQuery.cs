using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;

namespace Trsys.BackOffice.Application.Read.Queries
{
    public class UserReadModelSearchCountQuery : IQuery<int>
    {
    }

    public class UserReadModelSearchCountQueryHandler : IQueryHandler<UserReadModelSearchCountQuery, int>
    {
        private readonly IInMemoryReadStore<UserReadModel> readStore;

        public UserReadModelSearchCountQueryHandler(IInMemoryReadStore<UserReadModel> readStore)
        {
            this.readStore = readStore;
        }
        public async Task<int> ExecuteQueryAsync(UserReadModelSearchCountQuery query, CancellationToken cancellationToken)
        {
            var items = await readStore.FindAsync(d => true, cancellationToken);
            return items.Count;
        }
    }
}