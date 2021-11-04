using System;

namespace Trsys.Analytics.Abstractions
{
    public class TradeDurationDto
    {
        public DateTimeOffset? OpenedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }
        public TimeSpan Duration => OpenedAt.HasValue && ClosedAt.HasValue ? ClosedAt.Value - OpenedAt.Value : default;
    }
}
