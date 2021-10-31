using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Configuration;
using EventFlow.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.Application;

namespace Trsys.CopyTrading.Infrastructure
{
    public class CopyTradingEventFlowRootResolver : IDisposable
    {
        private readonly IRootResolver resolver;
        private bool disposedValue;

        public CopyTradingEventFlowRootResolver(IServiceProvider sp)
        {
            resolver = EventFlowOptions
                .New
                .UseCopyTradeApplication()
                .RegisterServices(sr => sr.Register(context => sp.GetRequiredService<ICopyTradingEventBus>()))
                .AddSubscribers(typeof(AllEventSubscriber))
                .AddAspNetCore()
                .CreateResolver();
        }

        public T Resolve<T>()
        {
            return resolver.Resolve<T>();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    resolver.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}