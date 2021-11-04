﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.Analytics.Abstractions
{
    public interface IAnalyticsService
    {
        Task<CopyTradeDto?> FindCopyTradeByIdAsync(string copyTradeId, CancellationToken cancellationToken);
        Task OpenCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, string symbol, string orderType, CancellationToken cancellationToken);
        Task CloseCopyTradeAsync(string copyTradeId, DateTimeOffset timestamp, CancellationToken cancellationToken);
    }
}
