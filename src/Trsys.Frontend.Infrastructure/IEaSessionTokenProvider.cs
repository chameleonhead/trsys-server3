namespace Trsys.Frontend.Infrastructure
{
    public interface IEaSessionTokenProvider
    {
        string GenerateToken(string id, string key, string keyType);
    }
}
