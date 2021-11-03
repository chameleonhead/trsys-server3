using System;

namespace Trsys.Frontend.Infrastructure.LogParsing
{
    public interface ILogInfo
    {
        public string Id { get; }
        public DateTimeOffset Timestamp { get; }
        public string Type { get; }
    }
}
