using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Ea.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public class EaEventHandler : BackgroundService
    {
        private readonly IEaService service;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public EaEventHandler(IEaService service, BackOfficeEventFlowRootResolver resolver)
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
                    // await service.SubscribeToEaEventsAsync(OnEaEvent, stoppingToken);
                }
                catch
                {
                }
            }
        }

        //private Task OnEaEvent(IEaEvent eaEvent)
        //{
        //    return Task.CompletedTask;
        //}

    }
}
