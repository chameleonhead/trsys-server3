using EventFlow.Entities;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeEntity : Entity<CopyTradeId>
    {
        public CopyTradeEntity(CopyTradeId id, List<SubscriberId> subscribers) : base(id)
        {
            Subscribers = subscribers;
        }

        public List<SubscriberId> Subscribers { get; }
    }
}
