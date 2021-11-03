using Trsys.Frontend.Abstractions;

namespace Trsys.Frontend.Infrastructure
{
    public interface IEaSessionTokenParser
    {
        EaSession ExtractToken(string token);
    }
}