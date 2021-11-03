using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Frontend.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public class FrontendEventHandler : BackgroundService
    {
        private readonly ILogger<FrontendEventHandler> logger;
        private readonly IFrontendEventBus eventBus;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public FrontendEventHandler(ILogger<FrontendEventHandler> logger, IFrontendEventBus eventBus, BackOfficeEventFlowRootResolver resolver)
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
                    await eventBus.Subscribe(OnFrontendEvent, stoppingToken);
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }
        }

        private async void OnFrontendEvent(IFrontendEvent eaEvent)
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
