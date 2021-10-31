using System;
using System.Threading.Tasks;
using Trsys.Ea.Domain;

namespace Trsys.Ea
{
    public interface IEaService
    {
        Task<SecretKeyDto> FindByKeyAsync(string key, string keyType);
        Task AddSecretKeyAsync(string distributionGroupId, string key, string keyType);
        Task RemvoeSecretKeyAsync(string distributionGroupId, string key, string keyType);
        Task<EaSession> GenerateSessionTokenAsync(string key, string keyType);
        Task DiscardSessionTokenAsync(string token, string key, string keyType);
        Task ValidateSessionTokenAsync(string token, string key, string keyType);
        Task PublishOrderTextAsync(DateTimeOffset timestamp, string key, string text);
        Task<OrderText> GetCurrentOrderTextAsync(string key);
        Task SubscribeOrderTextAsync(DateTimeOffset timestamp, string key, string text);
        Task ReceiveLogAsync(DateTimeOffset serverTimestamp, string key, string keyType, string version, string token, string text);
    }
}
