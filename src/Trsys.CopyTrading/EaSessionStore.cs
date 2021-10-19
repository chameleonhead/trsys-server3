using System.Collections.Generic;

namespace Trsys.CopyTrading
{
    public class EaSessionStore : IEaSessionStore
    {
        private readonly Dictionary<string, string> activeSessions = new();

        public void ClearActiveSession(EaSession session)
        {
            if (activeSessions.TryGetValue(session.Id, out var activeToken) && activeToken == session.Token)
            {
                activeSessions.Remove(session.Id);
            }
        }

        public void SetActiveSession(EaSession session)
        {
            activeSessions[session.Id] = session.Token;
        }

        public bool SetActiveSessionIfActiveSessionNotExists(EaSession session)
        {
            if (!activeSessions.TryGetValue(session.Id, out var activeToken))
            {
                activeSessions.Add(session.Id, session.Token);
                return true;
            }
            return activeToken == session.Token;
        }
    }
}
