namespace Trsys.Frontend.Infrastructure
{
    public interface IEaSessionStore
    {
        void SetActiveSession(string id, string token);
        void ClearActiveSession(string id);
        bool SetActiveSessionIfActiveSessionNotExists(string id, string token);
        void ClearActiveSessionTokenIfExists(string id, string token);
    }
}
