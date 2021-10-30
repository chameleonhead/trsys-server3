using System;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Configuration;
using Trsys.CopyTrading.Application;

namespace Trsys.CopyTrading
{
    public class CopyTradeEventFlowResolver : IDisposable
    {
        private readonly IRootResolver resolver;
        private bool disposedValue;

        public CopyTradeEventFlowResolver()
        {
            resolver = EventFlowOptions
                .New
                .UseCopyTradeApplication()
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