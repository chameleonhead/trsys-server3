using System;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Abstractions
{
    public class CopyTradeDto
    {
        public class TradeOrderDto
        {
            public string Id { get; set; }
            public DateTimeOffset OpenDistributedTimestamp { get; set; }
            public DateTimeOffset? CloseDistributedTimestamp { get; set; }
            public bool IsOpen { get; set; }
        }

        public string Id { get; set; }
        public string DistributionGroupId { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public List<string> Subscribers { get; set; }
        public DateTimeOffset OpenPublishedTimestamp { get; set; }
        public DateTimeOffset? ClosePublishedTimestamp { get; set; }
        public List<TradeOrderDto> TradeOrders { get; set; }
        public bool IsOpen { get; set; }
    }
}