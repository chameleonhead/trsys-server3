using Trsys.Ea.Abstractions;

namespace Trsys.Ea.Infrastructure
{
    public interface IEaSessionTokenParser
    {
        EaSession ExtractToken(string token);
    }
}