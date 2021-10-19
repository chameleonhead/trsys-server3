namespace Trsys.CopyTrading
{
    public interface IEaSessionManager
    {
        EaSession CreateSession(string id, string key, string keyType);
        bool ValidateToken(string key, string keyType, string token);
    }
}