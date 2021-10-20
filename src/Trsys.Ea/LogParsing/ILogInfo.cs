using System;

namespace Trsys.Ea.LogParsing
{
    public interface ILogInfo
    {
        public string Id { get; }
        public DateTimeOffset Timestamp { get; }
        public string Type { get; }
    }
}
