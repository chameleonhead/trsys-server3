using System.Collections.Generic;

namespace Trsys.Ea.Infrastructure
{
    public class EaSessionStore : IEaSessionStore
    {
        private readonly Dictionary<string, string> activeSessions = new();

        public void SetActiveSession(string id, string token)
        {
            activeSessions[id] = token;
        }

        public void ClearActiveSession(string id)
        {
            activeSessions.Remove(id);
        }

        public void ClearActiveSessionTokenIfExists(string id, string token)
        {
            if (activeSessions.TryGetValue(id, out var activeToken) && activeToken == token)
            {
                activeSessions.Remove(id);
            }
        }

        public bool SetActiveSessionIfActiveSessionNotExists(string id, string token)
        {
            if (!activeSessions.TryGetValue(id, out var activeToken))
            {
                activeSessions.Add(id, token);
                return true;
            }
            return activeToken == token;
        }
    }
}
