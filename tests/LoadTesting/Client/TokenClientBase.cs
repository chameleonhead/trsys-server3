﻿using LoadTesting.Extensions;
using LoadTesting.Server;
using NBomber.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTesting.Client
{
    public abstract class TokenClientBase
    {
        protected HttpClientPool ClientPool { get; }
        protected string SecretKey { get; }
        protected string KeyType { get; }
        protected string Token { get; private set; }

        private bool isInit = false;

        public TokenClientBase(HttpClientPool pool, string secretKey, string keyType)
        {
            SecretKey = secretKey;
            KeyType = keyType;
            ClientPool = pool;
        }

        public async Task InitializeAsync()
        {
            Token = await ClientPool.UseClientAsync(client => client.GenerateTokenAsync(SecretKey, KeyType));
            isInit = true;
        }

        public async Task FinalizeAsync()
        {
            await ClientPool.UseClientAsync(client => client.InvalidateTokenAsync(SecretKey, KeyType, Token));
            Token = null;
            isInit = false;
        }

        public async Task<Response> ExecuteAsync(CancellationToken cancellationToken)
        {
            EnsureInited();
            cancellationToken.ThrowIfCancellationRequested();
            return await OnExecuteAsync(cancellationToken);
        }

        private void EnsureInited()
        {
            if (!isInit) throw new InvalidOperationException("not initialized");
        }

        protected abstract Task<Response> OnExecuteAsync(CancellationToken cancellationToken);
    }
}
