using System.Collections.Generic;

namespace Trsys.CopyTrading
{
    public class EaSessionManager : IEaSessionManager
    {
        private readonly IEaSessionTokenProvider tokenProvider;
        private readonly IEaSessionTokenParser tokenParser;
        private readonly Dictionary<string, Dictionary<string, EaSession>> sessionsById = new();
        private readonly Dictionary<string, string> activeSessions = new();

        public EaSessionManager(IEaSessionTokenProvider tokenProvider, IEaSessionTokenParser tokenParser)
        {
            this.tokenProvider = tokenProvider;
            this.tokenParser = tokenParser;
        }

        public EaSession CreateSession(string id, string key, string keyType)
        {
            var token = tokenProvider.GenerateToken(id, key, keyType);
            var session = new EaSession(id, key, keyType, token);
            if (!sessionsById.TryGetValue(id, out var sessions))
            {
                sessions = new();
                sessionsById.Add(id, sessions);
            }
            sessions.Add(token, session);
            return session;
        }

        public bool ValidateToken(string key, string keyType, string token)
        {
            var session = tokenParser.ExtractToken(token);
            if (session.Key == key && session.KeyType == keyType)
            {
                if (!activeSessions.TryGetValue(session.Id, out var activeToken))
                {
                    activeSessions.Add(session.Id, token);
                    return true;
                }
                return activeToken == token;
            }
            return false;
        }
    }
}
