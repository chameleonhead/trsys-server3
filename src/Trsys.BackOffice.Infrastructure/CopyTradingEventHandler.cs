using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public class CopyTradingEventHandler : BackgroundService
    {
        private readonly ICopyTradingEventBus eventBus;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public CopyTradingEventHandler(ICopyTradingEventBus eventBus, BackOfficeEventFlowRootResolver resolver)
        {
            this.eventBus = eventBus;
            this.resolver = resolver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await eventBus.Subscribe(OnCopyTradeEvent, stoppingToken);
                }
                catch
                {
                }
            }
        }

        private async void OnCopyTradeEvent(ICopyTradingEvent copyTradingEvent)
        {
        }
    }
}
