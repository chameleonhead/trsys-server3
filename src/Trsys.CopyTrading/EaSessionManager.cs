using System.Threading.Tasks;

namespace Trsys.CopyTrading
{
    public class EaSessionManager : IEaSessionManager
    {
        private readonly IEaSessionTokenProvider tokenProvider;
        private readonly IEaSessionTokenParser tokenParser;
        private readonly IEaSessionStore sessionStore;

        public EaSessionManager(IEaSessionTokenProvider tokenProvider, IEaSessionTokenParser tokenParser, IEaSessionStore sessionStore)
        {
            this.tokenProvider = tokenProvider;
            this.tokenParser = tokenParser;
            this.sessionStore = sessionStore;
        }

        public Task<EaSession> CreateSessionAsync(string id, string key, string keyType)
        {
            var token = tokenProvider.GenerateToken(id, key, keyType);
            var session = new EaSession(id, key, keyType, token);
            if (sessionStore.SetActiveSessionIfActiveSessionNotExists(session.Id, session.Token))
            {
                return Task.FromResult(session);
            }
            throw new EaSessionAlreadyExistsException();
        }

        public Task DestroySessionAsync(string key, string keyType, string token)
        {
            var session = tokenParser.ExtractToken(token);
            sessionStore.ClearActiveSessionTokenIfExists(session.Id, session.Token);
            return Task.CompletedTask;
        }

        public Task DestroySessionAsync(string id)
        {
            sessionStore.ClearActiveSession(id);
            return Task.CompletedTask;
        }

        public Task<bool> ValidateTokenAsync(string key, string keyType, string token)
        {
            var session = tokenParser.ExtractToken(token);
            if (session.Key == key && session.KeyType == keyType)
            {
                if (sessionStore.SetActiveSessionIfActiveSessionNotExists(session.Id, session.Token))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
