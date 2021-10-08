using System;
using System.Threading.Tasks;

namespace Trsys.CopyTrading
{
    public class EaService : IEaService
    {
        public Task AddSecretKeyAsync(string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task DiscardSessionTokenAsync(string token, string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task<EaSession> GenerateSessionTokenAsync(string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task<OrderText> GetCurrentOrderTextAsync()
        {
            throw new NotImplementedException();
        }

        public Task PublishOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, string key, string keyType, string version, string token, string text)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, long eaTimestamp, string key, string keyType, string version, string token, string text)
        {
            throw new NotImplementedException();
        }

        public Task RemvoeSecretKeyAsync(string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOrderTextUpdated(Action<OrderText> handler)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOrderTextUpdated(Action<OrderText> handler)
        {
            throw new NotImplementedException();
        }

        public Task ValidateSessionTokenAsync(string token, string key, string keyType)
        {
            throw new NotImplementedException();
        }
    }
}
