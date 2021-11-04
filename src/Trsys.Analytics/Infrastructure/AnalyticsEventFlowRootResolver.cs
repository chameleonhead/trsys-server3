using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Configuration;
using System;
using Trsys.Analytics.Application;

namespace Trsys.Analytics.Infrastructure
{
    public class AnalyticsEventFlowRootResolver : IDisposable
    {
        private readonly IRootResolver resolver;
        private bool disposedValue;

        public AnalyticsEventFlowRootResolver()
        {
            resolver = EventFlowOptions
                .New
                .UseAnalyticsApplication()
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