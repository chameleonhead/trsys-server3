using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.CopyTrading.Infrastructure
{
    public class AllEventBus : ICopyTradingEventBus
    {
        private readonly List<Action<ICopyTradingEvent>> handlers = new();

        public void Publish(ICopyTradingEvent e)
        {
            foreach (var handler in handlers.ToArray())
            {
                handler.Invoke(e);
            }
        }

        public Task Subscribe(Action<ICopyTradingEvent> handler, CancellationToken cancellationToken)
        {
            TaskCompletionSource tcs = new TaskCompletionSource();
            handlers.Add(handler);
            cancellationToken.Register(() => {
                handlers.Remove(handler);
                tcs.SetResult();
            });
            return tcs.Task;
        }
    }
}
