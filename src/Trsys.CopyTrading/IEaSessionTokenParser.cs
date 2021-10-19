namespace Trsys.CopyTrading
{
    public interface IEaSessionTokenParser
    {
        EaSession ExtractToken(string token);
    }
}