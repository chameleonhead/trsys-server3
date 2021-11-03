using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Frontend.Abstractions
{
    public interface IFrontendEventBus
    {
        Task Subscribe(Action<IFrontendEvent> handler, CancellationToken cancellationToken);
        void Publish(IFrontendEvent e);
    }
}
