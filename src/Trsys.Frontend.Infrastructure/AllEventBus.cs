using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Frontend.Abstractions;

namespace Trsys.Frontend.Infrastructure
{
    public class AllEventBus : IEaEventBus
    {
        private readonly List<Action<IEaEvent>> handlers = new();

        public void Publish(IEaEvent e)
        {
            foreach (var handler in handlers.ToArray())
            {
                handler.Invoke(e);
            }
        }

        public Task Subscribe(Action<IEaEvent> handler, CancellationToken cancellationToken)
        {
            TaskCompletionSource tcs = new();
            handlers.Add(handler);
            cancellationToken.Register(() => {
                handlers.Remove(handler);
                tcs.SetResult();
            });
            return tcs.Task;
        }
    }
}
