namespace Trsys.Ea.Abstractions
{
    public class EaSession
    {
        public EaSession(string id, string key, string keyType, string token)
        {
            Id = id;
            Key = key;
            KeyType = keyType;
            Token = token;
        }

        public string Id { get; }
        public string Key { get; }
        public string KeyType { get; }
        public string Token { get; }
    }
}