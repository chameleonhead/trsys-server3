using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public class CopyTradingEventHandler : BackgroundService
    {
        private readonly ICopyTradingService service;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public CopyTradingEventHandler(ICopyTradingService service, BackOfficeEventFlowRootResolver resolver)
        {
            this.service = service;
            this.resolver = resolver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await service.SubscribeToCopyTradeEventsAsync(OnCopyTradeEvent, stoppingToken);
                }
                catch
                {
                }
            }
        }

        private Task OnCopyTradeEvent(ICopyTradingEvent copyTradingEvent)
        {
            return Task.CompletedTask;
        }

    }
}
