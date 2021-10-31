using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Ea.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public class EaEventHandler : BackgroundService
    {
        private readonly IEaEventBus eventBus;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public EaEventHandler(IEaEventBus eventBus, BackOfficeEventFlowRootResolver resolver)
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
                    await eventBus.Subscribe(OnEaEvent, stoppingToken);
                }
                catch
                {
                }
            }
        }

        private async void OnEaEvent(IEaEvent eaEvent)
        {
        }
    }
}
