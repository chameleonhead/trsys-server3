using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Frontend.Abstractions
{
    public interface IEaEventBus
    {
        Task Subscribe(Action<IEaEvent> handler, CancellationToken cancellationToken);
        void Publish(IEaEvent e);
    }
}
