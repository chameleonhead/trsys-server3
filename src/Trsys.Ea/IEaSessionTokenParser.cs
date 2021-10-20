namespace Trsys.Ea
{
    public interface IEaSessionTokenParser
    {
        EaSession ExtractToken(string token);
    }
}