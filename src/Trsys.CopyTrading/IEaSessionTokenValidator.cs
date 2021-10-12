namespace Trsys.CopyTrading
{
    public interface IEaSessionTokenValidator
    {
        bool ValidateToken(string key, string keyType, string token);
    }
}