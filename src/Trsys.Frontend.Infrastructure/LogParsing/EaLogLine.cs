using System;

namespace Trsys.Frontend.Infrastructure.LogParsing
{
    public class EaLogLine
    {
        public DateTimeOffset? Timestamp { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
    }
}