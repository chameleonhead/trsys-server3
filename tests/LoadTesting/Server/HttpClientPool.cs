using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTesting.Server
{
    public class HttpClientPool
    {
        private readonly List<Lazy<HttpClient>> clients;
        private int index = 0;

        public HttpClientPool(Func<HttpClient> clientFactory, int poolCount)
        {
            var clients = new List<Lazy<HttpClient>>();
            for (int i = 0; i < poolCount; i++)
            {
                clients.Add(new Lazy<HttpClient>(clientFactory));
            }
            this.clients = clients;
        }

        public async Task UseClientAsync(Func<HttpClient, Task> task)
        {
            var nextIndex = Interlocked.Increment(ref index);
            var client = clients[nextIndex % clients.Count];
            await task.Invoke(client.Value);
        }

        public async Task<T> UseClientAsync<T>(Func<HttpClient, Task<T>> task)
        {
            var nextIndex = Interlocked.Increment(ref index);
            var client = clients[nextIndex % clients.Count];
            return await task.Invoke(client.Value);
        }
    }
}
