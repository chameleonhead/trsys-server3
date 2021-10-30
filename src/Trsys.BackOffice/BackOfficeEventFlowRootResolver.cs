using System;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Configuration;
using Trsys.BackOffice.Application;

namespace Trsys.BackOffice
{
    public class BackOfficeEventFlowRootResolver : IDisposable
    {
        private readonly IRootResolver resolver;
        private bool disposedValue;

        public BackOfficeEventFlowRootResolver()
        {
            resolver = EventFlowOptions
                .New
                .UseBackOfficeApplication()
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