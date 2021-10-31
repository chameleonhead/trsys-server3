using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.CopyTrading.Abstractions
{
    public interface ICopyTradingEventBus
    {
        Task Subscribe(Action<ICopyTradingEvent> handler, CancellationToken cancellationToken);
        void Publish(ICopyTradingEvent e);
    }
}
