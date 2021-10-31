using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Ea.Abstractions
{
    public interface IEaEventBus
    {
        Task Subscribe(Action<IEaEvent> handler, CancellationToken cancellationToken);
        void Publish(IEaEvent e);
    }
}
