namespace Trsys.CopyTrading
{
    public interface IEaSessionStore
    {
        void SetActiveSession(EaSession session);
        bool SetActiveSessionIfActiveSessionNotExists(EaSession session);
        void ClearActiveSession(EaSession session);
    }
}
