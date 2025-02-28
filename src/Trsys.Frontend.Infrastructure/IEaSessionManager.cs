﻿using System.Threading.Tasks;
using Trsys.Frontend.Abstractions;

namespace Trsys.Frontend.Infrastructure
{
    public interface IEaSessionManager
    {
        Task<EaSession> CreateSessionAsync(string id, string key, string keyType);
        Task<bool> ValidateTokenAsync(string key, string keyType, string token);
        Task DestroySessionAsync(string key, string keyType, string token);
        Task DestroySessionAsync(string id);
    }
}