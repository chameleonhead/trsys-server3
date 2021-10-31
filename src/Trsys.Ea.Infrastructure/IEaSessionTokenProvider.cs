namespace Trsys.Ea.Infrastructure
{
    public interface IEaSessionTokenProvider
    {
        string GenerateToken(string id, string key, string keyType);
    }
}
