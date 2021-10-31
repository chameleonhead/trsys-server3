using System;

namespace Trsys.Ea.Infrastructure.LogParsing
{
    public class DeinitLog : LogBase
    {
        public DeinitLog()
        {
        }

        public DeinitLog(DateTimeOffset timestamp, string key, string keyType, string version, string token, int reason) : base(timestamp, key, keyType, version, token)
        {
            Reason = reason;
        }

        public int Reason { get; set; }
    }
}
