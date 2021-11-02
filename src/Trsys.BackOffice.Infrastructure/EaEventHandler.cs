using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Ea.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public class EaEventHandler : BackgroundService
    {
        private readonly ILogger<EaEventHandler> logger;
        private readonly IEaEventBus eventBus;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public EaEventHandler(ILogger<EaEventHandler> logger, IEaEventBus eventBus, BackOfficeEventFlowRootResolver resolver)
        {
            this.logger = logger;
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
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }
        }

        private async void OnEaEvent(IEaEvent eaEvent)
        {
            try
            {
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        }
    }
}
