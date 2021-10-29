using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public class CopyTradeService : ICopyTradeService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public CopyTradeService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public Task<PagedResult<CopyTradeDto>> SearchAsync(bool openOnly, int page, int perPage, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<CopyTradeDto> FindByIdAsync(string copyTradeId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task CloseAsync(string copyTradeId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> OpenAsync(string distributionGroupId, string symbol, string orderType, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}