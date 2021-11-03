﻿using System;

namespace Trsys.Frontend.Infrastructure.LogParsing
{
    public class OrderCloseExecutingLog : LogBase
    {
        public OrderCloseExecutingLog()
        {
        }

        public OrderCloseExecutingLog(DateTimeOffset timestamp, string key, string keyType, string version, string token, long serverTicketNo, long localTicketNo) : base(timestamp, key, keyType, version, token)
        {
            ServerTicketNo = serverTicketNo;
            LocalTicketNo = localTicketNo;
        }

        public long ServerTicketNo { get; set; }
        public long LocalTicketNo { get; set; }
    }
}